<script setup lang="ts">
const userName = defineModel<string>('userName');
const amount = defineModel<number>('amount');
const props = defineProps<{ loading: boolean }>();
const emit = defineEmits<{ (e: 'load'): void; (e: 'save'): void }>();
</script>

<template>
  <section class="card">
    <h2>Jugador</h2>

    <div class="row">
      <label>Nombre</label>
      <input v-model="userName" placeholder="tu nombre" />
    </div>

    <div class="row">
      <label>Saldo</label>
      <input type="number" step="0.01" v-model.number="amount" />
    </div>

    <div class="row">
      <button :disabled="props.loading" @click="emit('load')">Cargar saldo</button>
      <button :disabled="props.loading" @click="emit('save')">Guardar saldo</button>
    </div>
  </section>
</template>
