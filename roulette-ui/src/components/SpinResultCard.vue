<script setup lang="ts">
import type { SpinResultDto } from '../types/roulette';

defineProps<{
    result: SpinResultDto | null;
    loading: boolean;
}>();

defineEmits<{
    (e: 'spin'): void;
    (e: 'calculate'): void;
}>();
</script>

<template>
    <section class="card">
        <h2>Juego</h2>
        <div class="row">
            <button :disabled="loading" @click="$emit('spin')">Girar</button>
            <span v-if="result">Resultado: {{ result.number }} ({{ result.color }}, {{ result.parity }})</span>
        </div>
        <div class="row">
            <button :disabled="loading || !result" @click="$emit('calculate')">Calcular premio</button>
        </div>
    </section>
</template>

<style scoped>
.card {
    border: 1px solid #e5e7eb;
    border-radius: 12px;
    padding: 16px;
    margin-bottom: 16px
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
