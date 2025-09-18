<script setup lang="ts">
import type { SpinResultDto } from '../types/roulette';

defineProps<{
    loading: boolean;
    outcome: SpinResultDto | null;
    lastWon: boolean | null;
    lastPrize: number | null;
}>();

defineEmits<{ (e: 'play'): void; (e: 'sample'): void }>();
</script>

<template>
    <section class="card">
        <h2>Juego</h2>

        <div class="row">
            <button :disabled="loading" @click="$emit('play')">Jugar (resolver apuesta)</button>
            <button :disabled="loading" @click="$emit('sample')">Giro de muestra</button>
        </div>

        <div class="row" v-if="outcome">
            <span>Último resultado: <strong>{{ outcome.number }}</strong> ({{ outcome.color }}, {{ outcome.parity
                }})</span>
        </div>

        <div class="row" v-if="lastWon !== null">
            <span v-if="lastWon" style="color:#16a34a">¡Ganaste! Premio: {{ (lastPrize ?? 0).toFixed(2) }}</span>
            <span v-else style="color:#dc2626">Perdiste</span>
        </div>
    </section>
</template>

<style scoped>
.card {
    border: 1px solid #e5e7eb;
    border-radius: 12px;
    padding: 16px;
    margin-bottom: 16px;
    text-align: left
}

.row {
    display: flex;
    gap: 12px;
    align-items: center;
    margin: 8px 0
}

button {
    padding: 8px 12px;
    border: 0;
    border-radius: 8px;
    background: #111827;
    color: #fff;
    cursor: pointer
}

button:disabled {
    background: #9ca3af;
    cursor: not-allowed
}
</style>
