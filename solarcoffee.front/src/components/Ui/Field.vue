<template>
  <div class="Field">
    <label :for="fieldName">{{ fieldName }}</label>
    <input type="text" v-model="inputValue" />
    <div class="Field__ErrorMessage">
      <div v-if="showErrors">
        <p v-for="m in validationState.messages" :key="m">{{ m }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";

interface ValidationObject {
  valid: boolean;
  messages: string[];
}
export default defineComponent({
  props: {
    value: {
      type: String,
    },
    fieldName: {
      type: String,
      required: true,
    },
    validationState: {
      type: Object as () => ValidationObject,
      required: true,
    },
  },
  emits: ["value-changed"],
  setup(props, ctx) {
    const inputValue = ref(props.value || "");
    const showErrors = ref(false);
    watch(props.validationState, () => {
      showErrors.value = true;
    });
    watch(inputValue, () => {
      ctx.emit("value-changed", inputValue);
    });
    return {
      inputValue,
      showErrors,
    };
  },
});
</script>

<style lang="scss" scoped>
.Field {
  &__ErrorMessage {
    color: red;
    font-size: 0.6rem;
  }

  input {
    width: 80%;
    height: 1.8rem;
    margin: 0.8rem 2rem 0.8rem 0.8rem;
    font-size: 1.1rem;
    line-height: 1.3rem;
    padding: 0.2rem;
    color: #555;
  }
  label {
    font-weight: bold;
    margin: 0.8rem;
    display: block;
  }
}
</style>