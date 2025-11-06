<template>
  <div class="permissions-grid">
    <div v-for="group in permissions" :key="group.group" class="permission-group p-3">
      <h6 class="group-title">{{ group.group }}</h6>
      <div class="form-check" v-for="perm in group.permissions" :key="perm">
        <input class="form-check-input" type="checkbox"
               :id="idPrefix + perm"
               :value="perm"
               :checked="selectedPermissions.includes(perm)"
               @change="onToggle(perm)">
        <label class="form-check-label" :for="idPrefix + perm">{{ perm }}</label>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { defineProps, defineEmits } from 'vue'

  const props = defineProps({
    permissions: { type: Array, default: () => [] },
    selectedPermissions: { type: Array, default: () => [] },
    idPrefix: { type: String, default: '' }
  })

  const emit = defineEmits(['update:selectedPermissions'])

  const onToggle = (perm) => {
    const index = props.selectedPermissions.indexOf(perm)
    const updated = [...props.selectedPermissions]

    if (index === -1) updated.push(perm)
    else updated.splice(index, 1)

    emit('update:selectedPermissions', updated)
  }
</script>

<style scoped>
  .permissions-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
    gap: 1rem;
  }

  .permission-group {
    border-radius: 0.5rem;
    padding: 1rem;
    border: 1px solid #e0e0e0;
  }

  .group-title {
    font-weight: 600;
    font-size: 0.95rem;
    margin-bottom: 0.5rem;
    color: #333;
  }
</style>
