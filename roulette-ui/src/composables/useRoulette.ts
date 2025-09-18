// src/composables/useRoulette.ts
import { ref, computed } from 'vue';
import { spin, calculatePrize, getBalance, saveBalance } from '../api/roulette.api';
import type { CalculatePrizeRequest, SpinResultDto } from '../types/roulette';

type BetKind = 'Color' | 'ParityOfColor' | 'NumberAndColor';

export function useRoulette() {
  const userName = ref<string>('');
  const amount = ref<number>(0);

  const lastSpin = ref<SpinResultDto | null>(null);
  const betType = ref<BetKind>('Color');
  const selection = ref<{ color: 'red' | 'black'; parity: 'even' | 'odd'; number: number }>({
    color: 'red',
    parity: 'even',
    number: 0,
  });
  const stake = ref<number>(0);
  const loading = ref<boolean>(false);
  const message = ref<string>('');

  const canSpin = computed(() => !loading.value);
  const canCalc = computed(() => !!lastSpin.value && stake.value > 0 && !loading.value);

  function buildCalculateRequest(): CalculatePrizeRequest {
    const typeNum: 0 | 1 | 2 = betType.value === 'Color' ? 0 : betType.value === 'ParityOfColor' ? 1 : 2;
    return {
      betType: typeNum,
      stake: Number(stake.value),
      selection: {
        color:
          betType.value === 'Color' || betType.value === 'ParityOfColor' || betType.value === 'NumberAndColor'
            ? selection.value.color
            : undefined,
        parity: betType.value === 'ParityOfColor' ? selection.value.parity : undefined,
        number: betType.value === 'NumberAndColor' ? Number(selection.value.number) : undefined,
      },
      spin: lastSpin.value!, // ya validamos antes que exista
    };
  }

  async function doSpin() {
    message.value = '';
    loading.value = true;
    try {
      lastSpin.value = await spin();
    } catch (e) {
      console.error(e);
      message.value = 'Error al girar la ruleta.';
    } finally {
      loading.value = false;
    }
  }

  async function doCalculate() {
    if (!lastSpin.value) {
      message.value = 'Primero realiza un giro.';
      return;
    }
    if (stake.value <= 0) {
      message.value = 'La apuesta (stake) debe ser > 0.';
      return;
    }
    loading.value = true;
    message.value = '';
    try {
      const req = buildCalculateRequest();
      const res = await calculatePrize(req);
      amount.value = Number(amount.value) + Number(res.net);
      message.value = res.won ? `¡Ganaste! +${res.payout} (neto: +${res.net}).` : `Perdiste -${Math.abs(res.net)}.`;
    } catch (e) {
      console.error(e);
      message.value = 'Error al calcular el premio.';
    } finally {
      loading.value = false;
    }
  }

  async function doLoadBalance() {
    if (!userName.value.trim()) {
      message.value = 'Escribe un nombre para cargar saldo.';
      return;
    }
    loading.value = true;
    message.value = '';
    try {
      const { balance } = await getBalance(userName.value.trim());
      amount.value = Number(balance ?? 0);
      message.value = `Saldo cargado: ${amount.value}.`;
    } catch (e) {
      console.error(e);
      message.value = 'Error al cargar saldo.';
    } finally {
      loading.value = false;
    }
  }

  async function doSaveBalance() {
    if (!userName.value.trim()) {
      message.value = 'Escribe un nombre para guardar saldo.';
      return;
    }
    loading.value = true;
    message.value = '';
    try {
      const { balance } = await saveBalance(userName.value.trim(), amount.value);
      amount.value = Number(balance);
      message.value = 'Saldo guardado en servidor.';
    } catch (e) {
      console.error(e);
      message.value = 'Error al guardar saldo.';
    } finally {
      loading.value = false;
    }
  }

  return {
    userName,
    amount,
    lastSpin,
    betType,
    selection,
    stake,
    loading,
    message,
    canSpin,
    canCalc,
    doSpin,
    doCalculate,
    doLoadBalance,
    doSaveBalance,
  };
}
