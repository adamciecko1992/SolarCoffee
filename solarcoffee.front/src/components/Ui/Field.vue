<template>
  <div class="Field">
    <label :for="fieldName">{{ fieldName }}</label>
    <input type="text" v-model="inputValue" @blur="touched = true" required />
    <span v-if="!valid && touched" class="Field__ErrorMessage">
      {{ localErrorMessage }}</span
    >
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
      required: true,
    },
  },
  emits: ["value-changed", "valid-changed"],
  setup(props, ctx) {
    const inputValue = ref(props.value || "");
    const localErrorMessage = ref("");
    const valid = ref(false);
    const touched = ref(false);

    watch(
      inputValue,
      debounce(() => {
        //dekonstrukcja
        const validatonResult = props.customValidator(inputValue.value)[0];
        localErrorMessage.value = props.customValidator(inputValue.value)[1];
        valid.value = validatonResult;
        ctx.emit("value-changed", inputValue.value);
      }, 500)
    );
    watch(valid, (isValid) => {
      if (isValid) {
        ctx.emit("valid-changed", true);
      } else {
        ctx.emit("valid-changed", false);
      }
    });

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