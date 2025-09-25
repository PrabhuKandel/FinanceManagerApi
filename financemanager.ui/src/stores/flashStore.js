import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useFlashStore = defineStore('flash', () => {
  const message = ref('')
  const type = ref('success') // optional: 'success' | 'error' etc.

  function setMessage(newMessage, newType = 'success') {
    message.value = newMessage
    type.value = newType
  }

  function clear() {
    message.value = ''
    type.value = 'success'
  }

  return { message, type, setMessage, clear }
})
