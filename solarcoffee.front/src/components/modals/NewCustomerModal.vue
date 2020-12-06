<template>
  <solar-modal>
    <template v-slot:header> Add New Customer </template>
    <template v-slot:body>
      <form class="newCustomer" @submit.prevent="save">
        <Field
          fieldName="Firstname"
          :customValidator="validators.onlyLettersNoSpaces"
          @value-changed="handleCustomerPropChange($event, 'firstName')"
          @valid-changed="updateValid($event, 'firstname')"
        />
        <li>
          <Field
            fieldName="Lastname"
            :customValidator="validators.onlyLettersNoSpaces"
            @value-changed="handleCustomerPropChange($event, 'lastName')"
          />
        </li>
        <li>
          <Field
            fieldName="Adress"
            :customValidator="validators.onlyLettersAndNumbersWithSpaces"
            @value-changed="handleAdressChange($event, 'adressLine1')"
          />
        </li>
        <li>
          <Field
            fieldName="Adress line 2 "
            :customValidator="validators.onlyLettersAndNumbersWithSpaces"
            @value-changed="handleAdressChange($event, 'adressLine2')"
          />
        </li>
        <Field
          fieldName="City"
          :customValidator="validators.onlyLettersWithSpaces"
          @value-changed="handleAdressChange($event, 'city')"
        />
        <li>
          <Field
            fieldName="State"
            :customValidator="validators.onlyLettersWithSpaces"
            @value-changed="handleAdressChange($event, 'state')"
          />
        </li>
        <li>
          <Field
            fieldName="Postal code"
            :customValidator="validators.onlyNumbersAndDashes"
            @value-changed="handleAdressChange($event, 'postalCode')"
          />
        </li>
        <li>
          <Field
            fieldName="Country"
            :customValidator="validators.onlyLettersAndNumbersWithSpaces"
            @value-changed="handleAdressChange($event, 'country')"
          />
        </li>
      </form>
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        aria-label="save new customer"
        :isDisabled="!valid"
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
import { defineComponent, reactive, ref } from "vue";
import SolarButton from "@/components/SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue";
import Field from "@/components/Ui/Field.vue";
import validators from "../../validation/Validators";
import { ICustomer } from "../../types/Customer";
import { ICustomerAdress } from "../../types/Customer";

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
    function updateValid(value: boolean, property: string) {
      validationState[property as keyof typeof validationState] = value;
      const validationStatValues = Object.values(validationState);
      const formValid = validationStatValues.every((val) => val === true);
      console.log(formValid);
      if (formValid) {
        valid.value = true;
      }
    }
    function handleCustomerPropChange(value: string, field: keyof ICustomer) {
      //wont work without never
      customer[field] = value as never;
    }

    function handleAdressChange(value: unknown, field: keyof ICustomerAdress) {
      //wont work without never
      customer.primaryAdress[field] = value as never;
    }

    function save() {
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