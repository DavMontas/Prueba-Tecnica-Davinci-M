<script setup lang="ts">
import { computed } from 'vue';

type BetKind = 'Color' | 'ParityOfColor' | 'NumberAndColor';

const betType = defineModel<BetKind>('betType', { required: true });
const selection = defineModel<{ color: 'red' | 'black'; parity: 'even' | 'odd'; number: number }>('selection', { required: true });
const stake = defineModel<number>('stake', { required: true });
const props = defineProps<{ loading: boolean }>();

const selectionColor = computed({
    get: () => selection.value.color,
    set: (v: 'red' | 'black') => { selection.value = { ...selection.value, color: v }; }
});

const selectionParity = computed({
    get: () => selection.value.parity,
    set: (v: 'even' | 'odd') => { selection.value = { ...selection.value, parity: v }; }
});

const selectionNumber = computed({
    get: () => selection.value.number,
    set: (v: number) => { selection.value = { ...selection.value, number: Number(v) }; }
});
</script>

<template>
    <section class="card">
        <h2>Apuesta</h2>

        <div class="row">
            <label>Tipo</label>
            <select v-model="betType">
                <option>Color</option>
                <option>ParityOfColor</option>
                <option>NumberAndColor</option>
            </select>
        </div>

        <div v-if="betType === 'Color'" class="row">
            <label>Color</label>
            <!-- 👇 usamos el wrapper -->
            <select v-model="selectionColor">
                <option value="red">red</option>
                <option value="black">black</option>
            </select>
        </div>

        <div v-if="betType === 'ParityOfColor'">
            <div class="row">
                <label>Color</label>
                <select v-model="selectionColor">
                    <option value="red">red</option>
                    <option value="black">black</option>
                </select>
            </div>
            <div class="row">
                <label>Paridad</label>
                <select v-model="selectionParity">
                    <option value="even">even</option>
                    <option value="odd">odd</option>
                </select>
            </div>
        </div>

        <div v-if="betType === 'NumberAndColor'">
            <div class="row">
                <label>Número (0-36)</label>
                <input type="number" min="0" max="36" v-model.number="selectionNumber" />
            </div>
            <div class="row">
                <label>Color</label>
                <select v-model="selectionColor">
                    <option value="red">red</option>
                    <option value="black">black</option>
                </select>
            </div>
        </div>

        <div class="row">
            <label>Stake</label>
            <input type="number" min="0.01" step="0.01" v-model.number="stake" />
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

label {
    width: 120px;
    font-weight: 600
}

input,
select {
    flex: 1;
    padding: 8px;
    border: 1px solid #d1d5db;
    border-radius: 8px
}
</style>
