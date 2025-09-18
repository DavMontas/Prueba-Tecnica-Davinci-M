import { ref, computed } from 'vue';
import { resolveBet, getBalance, saveBalance, spin as sampleSpin } from '../api/roulette.api';
import type {
  ApiBetType,
  BetKind,
  ResolveBetRequestDto,
  SpinResultDto,
} from '../types/roulette';

// Map de string UI -> enum numérico del backend
function betKindToApi(kind: BetKind): ApiBetType {
  switch (kind) {
    case 'Color': return 1;
    case 'ParityOfColor': return 2;
    case 'NumberAndColor': return 3;
    default: return 1;
  }
}

export function useRoulette() {
  // Usuario / saldo local (solo se persiste si "Guardar" = requisito)
  const userName = ref<string>('');
  const amount = ref<number>(0);

  // Apuesta
  const betType = ref<BetKind>('Color');
  const selection = ref<{ color: 'red' | 'black'; parity: 'even' | 'odd'; number: number }>({
    color: 'red',
    parity: 'even',
    number: 0,
  });
  const stake = ref<number>(0);

  // Resultado última jugada del servidor
  const lastOutcome = ref<SpinResultDto | null>(null);
  const lastPrize = ref<number | null>(null);
  const lastWon = ref<boolean | null>(null);

  // Estado UI
  const loading = ref<boolean>(false);
  const message = ref<string>('');

  const canPlay = computed(() => stake.value > 0 && !loading.value);
  const canSave = computed(() => userName.value.trim().length > 0 && !loading.value);

  // (Opcional) giro de muestra (no afecta premio ni backend)
  async function doSampleSpin() {
    loading.value = true;
    message.value = '';
    try {
      const sample: SpinResultDto = await sampleSpin();
      lastOutcome.value = sample;
      message.value = `Giro de muestra: ${sample.number} (${sample.color}, ${sample.parity}).`;
    } catch (e) {
      console.error(e);
      message.value = 'Error al realizar giro de muestra.';
    } finally {
      loading.value = false;
    }
  }

  function buildResolveRequest(): ResolveBetRequestDto {
    const apiBetType = betKindToApi(betType.value);
    return {
      betType: apiBetType,
      stake: Number(stake.value),
      selection: {
        color:
          betType.value === 'Color' ||
          betType.value === 'ParityOfColor' ||
          betType.value === 'NumberAndColor'
            ? selection.value.color
            : undefined,
        parity: betType.value === 'ParityOfColor' ? selection.value.parity : undefined,
        number: betType.value === 'NumberAndColor' ? Number(selection.value.number) : undefined,
      },
    };
  }

  async function doPlay() {
    if (stake.value <= 0) {
      message.value = 'La apuesta (stake) debe ser > 0.';
      return;
    }
    // Validaciones básicas de selección en cliente (el backend valida también)
    if (betType.value === 'NumberAndColor' &&
        (selection.value.number < 0 || selection.value.number > 36)) {
      message.value = 'El número debe estar en [0, 36].';
      return;
    }

    loading.value = true;
    message.value = '';
    try {
      const req = buildResolveRequest();
      const res = await resolveBet(req);

      lastOutcome.value = res.outcome;
      lastPrize.value = res.prize;
      lastWon.value = res.won;

      // Ajuste de saldo de la "partida local" (NO se persiste hasta Guardar)
      amount.value = Number((amount.value + res.net).toFixed(2));

      message.value = res.won
        ? `¡Ganaste! Premio: +${res.prize.toFixed(2)} | Neto: ${res.net >= 0 ? '+' : ''}${res.net.toFixed(2)} | Resultado: ${res.outcome.number} (${res.outcome.color}, ${res.outcome.parity})`
        : `Perdiste ${Math.abs(res.net).toFixed(2)} | Resultado: ${res.outcome.number} (${res.outcome.color}, ${res.outcome.parity})`;
    } catch (e: any) {
      console.error(e);
      message.value = e?.response?.data ?? 'Error al resolver la apuesta.';
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
      message.value = `Saldo cargado para "${userName.value.trim()}": ${amount.value.toFixed(2)}.`;
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
    // state
    userName, amount, betType, selection, stake,
    lastOutcome, lastPrize, lastWon,
    loading, message,

    // computed
    canPlay, canSave,

    // actions
    doPlay, doSampleSpin, doLoadBalance, doSaveBalance,
  };
}
