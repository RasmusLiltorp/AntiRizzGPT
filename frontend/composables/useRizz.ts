import { ref, computed } from 'vue'
import type { RizzRequest, RizzResponse } from './rizz'

export function useRizz() {
  const input = ref('')
  const response = ref('')
  const loading = ref(false)
  const maxChars = 250

  const charCount = computed(() => input.value.length)
  const isNearLimit = computed(() => charCount.value > 220)
  const canSubmit = computed(() => charCount.value > 0 && !loading.value)

  async function sendRizz() {

    if (input.value.trim() === '') return

    loading.value = true
    response.value = ''

    try {
        const payload: RizzRequest = { message: input.value }

        const res = await $fetch<RizzResponse>(`http://localhost:5095/api/rizz`, {
        method: 'POST',
        body: payload,
        headers: {
            'Content-Type': 'application/json'
        }
        })

        response.value = res.response
    } catch (error: any) {
        console.error('Error sending rizz:', error)
        response.value = 'Oops! Noget gik galt. Prøv igen senere.'
    } finally {
        loading.value = false
    }
}


return {
    input,
    response,
    loading,
    maxChars,
    charCount,
    isNearLimit,
    canSubmit,
    sendRizz
  }
}
