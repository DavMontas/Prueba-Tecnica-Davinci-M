<script setup lang="ts">
const userName = defineModel<string>('userName', { required: true });
const amount = defineModel<number>('amount', { required: true });

const props = defineProps<{ loading: boolean }>();
const emit = defineEmits<{ (e: 'load'): void; (e: 'save'): void }>();
</script>

<template>
  <section class="card">
    <h2>Jugador</h2>

    <div class="row">
      <label>Nombre</label>
      <input v-model="userName" placeholder="tu nombre" :disabled="props.loading" />
    </div>

    <div class="row">
      <label>Saldo (local)</label>
      <input type="number" step="0.01" v-model.number="amount" :disabled="props.loading" />
    </div>

    <div class="row">
      <button :disabled="props.loading || !userName?.trim()" @click="emit('load')">Cargar saldo</button>
      <button :disabled="props.loading || !userName?.trim()" @click="emit('save')">Guardar saldo</button>
    </div>
    <p class="hint">El saldo se guarda solo cuando presionas "Guardar saldo".</p>
  </section>
</template>

<style scoped>
.card{border:1px solid #e5e7eb;border-radius:12px;padding:16px;margin-bottom:16px;text-align:left}
.row{display:flex;gap:12px;align-items:center;margin:8px 0}
label{width:140px;font-weight:600}
input,select{flex:1;padding:8px;border:1px solid #d1d5db;border-radius:8px}
button{padding:8px 12px;border:0;border-radius:8px;background:#111827;color:#fff;cursor:pointer}
button:disabled{background:#9ca3af;cursor:not-allowed}
.hint{color:#6b7280;font-size:.9rem;margin:6px 0 0}
</style>
