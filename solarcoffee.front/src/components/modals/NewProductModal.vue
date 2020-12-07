<template>
  <solar-modal>
    <template v-slot:header> Add New Product </template>
    <template v-slot:body>
      <ul class="newProduct">
        <li>
          <label for="isTaxable">Is this product taxable?</label>
          <input
            type="checkbox"
            id="isTaxable"
            v-model="newProduct.isTaxable"
          />
        </li>
        <li>
          <Field
            fieldName="Name"
            :customValidator="validators.onlyLettersAndNumbersWithSpaces"
            @value-changed="handleInput($event, 'name')"
            @valid-changed="updateValid($event, 'name', validationState)"
          />
        </li>

        <li>
          <Field
            fieldName="Description"
            :customValidator="validators.onlyLettersAndNumbersWithSpaces"
            @value-changed="handleInput($event, 'description')"
            @valid-changed="updateValid($event, 'description', validationState)"
          />
        </li>

        <li>
          <Field
            fieldName="Price"
            :customValidator="validators.onlyNumbers"
            @value-changed="handleInput($event, 'price')"
            @valid-changed="updateValid($event, 'price', validationState)"
          />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        @click="save"
        aria-label="save new item"
        :disabled="!isValid"
      >
        Save Product
      </solar-button>
      <solar-button type="button" @click="close" aria-label="close modal">
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, watch } from "vue";
import { IProduct } from "../../types/Product";
import updateValid from "../../helpers/updateValidationState";
import SolarButton from "@/components/SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue";
import Field from "../Ui/Field.vue";
import validators from "../../validation/Validators";

export default defineComponent({
  components: {
    SolarButton,
    SolarModal,
    Field,
  },
  emits: ["save-product", "close"],

  setup(_, ctx) {
    const newProduct: IProduct = reactive({
      createdOn: new Date(),
      updatedOn: new Date(),
      id: 0,
      description: "",
      isTaxable: false,
      name: "",
      price: 0,
      isArchived: false,
    });

    const validationState = reactive({
      description: false,
      name: false,
      price: false,
    });

    const isValid = ref(false);

    watch(validationState, () => {
      isValid.value = Object.values(validationState).every((v) => v);
    });

    function handleInput(value: string, field: keyof IProduct) {
      (newProduct as any)[field] = value;
    }

    function close() {
      ctx.emit("close");
    }
    function save() {
      ctx.emit("save-product", newProduct);
    }

    return {
      save,
      close,
      newProduct,
      validators,
      handleInput,
      updateValid,
      validationState,
      isValid,
    };
  },
});
</script>

<style scoped lang="scss">
.newProduct {
  list-style: none;
  padding: 0;
  margin: 0;
  input {
    width: 100%;
    height: 1.8rem;
    margin-bottom: 1.2rem;
    font-size: 1.1rem;
    line-height: 1.3rem;
    padding: 0.2rem;
    color: #555;
  }
  label {
    font-weight: bold;
    display: block;
    margin-bottom: 0.3rem;
  }
}
</style>
