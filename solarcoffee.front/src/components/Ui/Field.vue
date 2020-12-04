<template>
  <div class="Field">
    <label :for="fieldName">{{ fieldName }}</label>
    <input type="text" v-model="inputValue" @blur="touched = true" />
    <span v-if="!valid && touched" class="Field_ErrorMessage">
      {{ localErrorMessage }}</span
    >
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, PropType, watch } from "vue";
export default defineComponent({
  props: {
    fieldName: {
      type: String,
      required: true,
    },
    customValidator: {
      type: Function as PropType<(input: string) => [boolean, string]>,
      required: true,
    },
  },
  emits: ["value-change"],
  setup(props, ctx) {
    const inputValue = ref("");
    const localErrorMessage = ref("");
    const valid = ref(false);
    const touched = ref(false);

    const handleBlur = (): void => {
      touched.value = true;
    };

    watch(inputValue, () => {
      console.log(touched.value);
      const [validatonResult, errorMessage] = props.customValidator(
        inputValue.value
      );
      valid.value = validatonResult;
      localErrorMessage.value = errorMessage;
      if (valid.value) {
        ctx.emit("value-change", inputValue.value);
      }
    });

    return {
      inputValue,
      localErrorMessage,
      touched,
      valid,
      handleBlur,
    };
  },
});
</script>

<style lang="scss" scoped>
.Field {
  &__ErrorMessage {
    color: red;
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