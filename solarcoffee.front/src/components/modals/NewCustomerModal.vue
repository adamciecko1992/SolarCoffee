<template>
  <solar-modal>
    <template v-slot:header> Add New Customer </template>
    <template v-slot:body>
      <form class="newCustomer" @submit.prevent="save">
        <Field
          fieldName="Firstname"
          @value-changed="handleCustomerPropChange($event, 'firstName')"
          @valid-changed="updateValid($event, 'firstname', validationState)"
          :customAsyncValidator="validators.testingAsync"
        />
        <li>
          <Field
            fieldName="Lastname"
            :customValidator="validators.onlyLettersNoSpaces"
            @value-changed="handleCustomerPropChange($event, 'lastName')"
            @valid-changed="updateValid($event, 'lastname', validationState)"
          />
        </li>
        <li>
          <Field
            fieldName="Adress"
            :customValidator="validators.onlyLettersAndNumbersWithSpaces"
            @value-changed="handleAdressChange($event, 'adressLine1')"
            @valid-changed="updateValid($event, 'adressLine1', validationState)"
          />
        </li>
        <li>
          <Field
            fieldName="Adress line 2 "
            :customValidator="validators.onlyLettersAndNumbersWithSpaces"
            @value-changed="handleAdressChange($event, 'adressLine2')"
            @valid-changed="updateValid($event, 'adressLine2', validationState)"
          />
        </li>
        <Field
          fieldName="City"
          :customValidator="validators.onlyLettersWithSpaces"
          @value-changed="handleAdressChange($event, 'city')"
          @valid-changed="updateValid($event, 'city', validationState)"
        />
        <li>
          <Field
            fieldName="State"
            :customValidator="validators.onlyLettersWithSpaces"
            @value-changed="handleAdressChange($event, 'state')"
            @valid-changed="updateValid($event, 'state', validationState)"
          />
        </li>
        <li>
          <Field
            fieldName="Postal code"
            :customValidator="validators.onlyNumbersAndDashes"
            @value-changed="handleAdressChange($event, 'postalCode')"
            @valid-changed="updateValid($event, 'postalCode', validationState)"
          />
        </li>
        <li>
          <Field
            fieldName="Country"
            :customValidator="validators.onlyLettersWithSpaces"
            @value-changed="handleAdressChange($event, 'country')"
            @valid-changed="updateValid($event, 'country', validationState)"
          />
        </li>
      </form>
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        aria-label="save new customer"
        :isDisabled="!valid"
        @click="save"
      >
        Save Customer
      </solar-button>

      <solar-button type="button" @click="close" aria-label="Close modal">
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, watch } from "vue";
import SolarButton from "@/components/SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue";
import Field from "@/components/Ui/Field.vue";
import validators from "../../validation/Validators";
import { ICustomer } from "../../types/Customer";
import { ICustomerAdress } from "../../types/Customer";
import updateValid from "../../helpers/updateValidationState";

export default defineComponent({
  name: "NewCustomerModal",
  components: { SolarButton, SolarModal, Field },
  setup(_, ctx) {
    const customer: ICustomer = reactive({
      primaryAdress: {
        adressLine1: "",
        adressLine2: "",
        city: "",
        state: "",
        country: "",
        postalCode: "",
      },
      //new Date powinien byc po stronie backendu
      createdOn: new Date(),
      updatedOn: new Date(),
      firstName: "",
      lastName: "",
      id: 0,
    });

    const valid = ref(false);
    const validationState = reactive({
      firstname: false,
      lastname: false,
      adressLine1: false,
      adressLine2: false,
      city: false,
      state: false,
      country: false,
      postalCode: false,
    });
    watch(validationState, () => {
      valid.value = Object.values(validationState).every((v) => v);
      console.log(validationState);
    });

    function handleCustomerPropChange(value: string, field: keyof ICustomer) {
      (customer as any)[field] = value;
    }

    function handleAdressChange(value: unknown, field: keyof ICustomerAdress) {
      (customer as any).primaryAdress[field] = value;
    }

    function save() {
      console.log("save");
      ctx.emit("save-customer", customer);
    }
    function close() {
      ctx.emit("close");
    }
    return {
      save,
      close,
      customer,
      validators,
      handleCustomerPropChange,
      handleAdressChange,
      valid,
      updateValid,
      validationState,
    };
  },
});
</script>

<style scoped lang="scss">
.newCustomer {
  display: flex;
  flex-wrap: wrap;
  list-style: none;
  padding: 0;
  margin: 0;
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