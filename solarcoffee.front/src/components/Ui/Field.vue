<template>
  <div class="Field">
    <label :for="fieldName">{{ fieldName }}</label>
    <input type="text" v-model="inputValue" @blur="touched = true" required />
    <p v-if="!valid && touched" class="Field__ErrorMessage">
      {{ localErrorMessage }}
    </p>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, PropType, watch } from "vue";
import { debounce, values } from "lodash";
export default defineComponent({
  props: {
    value: {
      type: String,
    },
    fieldName: {
      type: String,
      required: true,
    },
    customValidator: {
      type: Function as PropType<(input: string) => [boolean, string]>,
    },
    customAsyncValidator: {
      type: Function as PropType<(input: string) => Promise<[boolean, string]>>,
    },
  },
  emits: ["value-changed", "valid-changed"],
  setup(props, ctx) {
    const inputValue = ref(props.value || "");
    const localErrorMessage = ref("");
    const valid = ref(false);
    const touched = ref(false);

    if (props.customValidator) {
      watch(inputValue, () => {
        console.log("wach sync");
        if (props.customValidator) {
          const [validationResult, errorMessage] = props.customValidator(
            inputValue.value
          );
          localErrorMessage.value = errorMessage;
          valid.value = validationResult;
          ctx.emit("value-changed", inputValue.value);
        }
      });
    }
    if (props.customAsyncValidator) {
      watch(
        inputValue,
        debounce(async () => {
          if (props.customAsyncValidator) {
            const [
              validationResult,
              errorMessage,
            ] = await props.customAsyncValidator(inputValue.value);
            localErrorMessage.value = errorMessage;
            valid.value = validationResult;
          }
        }, 1500)
      );
    }
    watch(valid, (isValid) =>
      isValid
        ? ctx.emit("valid-changed", true)
        : ctx.emit("valid-changed", false)
    );

    const handleBlur = (): void => {
      touched.value = true;
    };

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
    font-size: 0.8rem;
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