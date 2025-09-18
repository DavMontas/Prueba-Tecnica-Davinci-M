<script setup lang="ts">
import BalancePanel from '../components/BalancePanel.vue';
import BetForm from '../components/BetForm.vue';
import PlayPanel from '../components/PlayPanel.vue';
import { useRoulette } from '../composables/useRoulette';

const {
    userName, amount, betType, selection, stake,
    lastOutcome, lastPrize, lastWon,
    loading, message,
    doPlay, doSampleSpin, doLoadBalance, doSaveBalance,
} = useRoulette();
</script>

<template>
    <main class="container">
        <h1>Roulette Game</h1>

        <BalancePanel v-model:userName="userName" v-model:amount="amount" :loading="loading" @load="doLoadBalance"
            @save="doSaveBalance" />

        <BetForm v-model:betType="betType" v-model:selection="selection" v-model:stake="stake" :loading="loading" />

        <PlayPanel :loading="loading" :outcome="lastOutcome" :lastWon="lastWon" :lastPrize="lastPrize" @play="doPlay"
            @sample="doSampleSpin" />

        <p v-if="message" class="message">{{ message }}</p>
    </main>
</template>

<style scoped>
.container {
    max-width: 980px;
    margin: 0 auto;
    padding: 24px
}

h1 {
    margin: 0 0 18px
}

.message {
    margin-top: 12px;
    font-weight: 600
}
</style>
