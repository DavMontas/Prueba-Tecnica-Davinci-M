<script setup lang="ts">
import { useRoulette } from '../composables/useRoulette';
import BalancePanel from '../components/BalancePanel.vue';
import BetForm from '../components/BetForm.vue';
import SpinResultCard from '../components/SpinResultCard.vue';

const {
    userName, amount, lastSpin, betType, selection, stake, loading, message,
    doSpin, doCalculate, doLoadBalance, doSaveBalance,
} = useRoulette();
</script>

<template>
    <main class="container">
        <h1>Roulette Game</h1>

        <BalancePanel v-model:userName="userName" v-model:amount="amount" :loading="loading" @load="doLoadBalance"
            @save="doSaveBalance" />

        <BetForm v-model:betType="betType" v-model:selection="selection" v-model:stake="stake" :loading="loading" />

        <SpinResultCard :result="lastSpin" :loading="loading" @spin="doSpin" @calculate="doCalculate" />

        <p v-if="message" class="message">{{ message }}</p>
    </main>
</template>
