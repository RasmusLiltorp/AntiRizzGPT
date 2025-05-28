<template>
  <div class="space-y-4">
    <h1 class="text-4xl font-bold">AntiRizzGPT</h1>
    
    <div class="form-control w-full">
      <textarea 
        v-model="input" 
        placeholder="Skriv din rizz her..." 
        class="textarea textarea-bordered w-full" 
        rows="4"
        :maxlength="maxChars"
      />
      <div class="flex justify-end mt-1">
        <span class="text-sm" :class="{'text-error': isNearLimit}">
          {{ charCount }}/{{ maxChars }} tegn
        </span>
      </div>
    </div>
    
    <button @click="sendRizz" class="btn btn-primary" :disabled="!canSubmit">
      {{ loading ? 'Rizzer...' : 'Send rizz' }}
    </button>
    
    <div v-if="response" class="alert alert-info mt-4">
      <span>{{ response }}</span>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRizz } from '~/composables/useRizz'

const { 
  input, 
  response, 
  loading, 
  maxChars, 
  charCount, 
  isNearLimit, 
  canSubmit, 
  sendRizz: originalSendRizz 
} = useRizz()

async function sendRizz() {
  await originalSendRizz()
}
</script>