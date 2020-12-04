<template>
  <solar-modal>
    <template v-slot:header> Add New Customer </template>
    <template v-slot:body>
      <ul class="newCustomer">
        <li>
          <label for="firstName">First Name</label>
          <input type="text" id="firstName" v-model="customer.firstName" />
        </li>
        <li>
          <label for="lastName">Last Name</label>
          <input type="text" id="lastName" v-model="customer.lastName" />
        </li>
        <li>
          <label for="address1">Address Line 1</label>
          <input
            type="text"
            id="address1"
            v-model="customer.primaryAdress.adressLine1"
          />
        </li>
        <li>
          <label for="address2">Address Line 2</label>
          <input
            type="text"
            id="address2"
            v-model="customer.primaryAdress.adressLine2"
          />
        </li>
        <li>
          <label for="city">City</label>
          <input type="text" id="city" v-model="customer.primaryAdress.city" />
        </li>
        <li>
          <label for="state">State</label>
          <input
            type="text"
            id="state"
            v-model="customer.primaryAdress.state"
          />
        </li>
        <li>
          <label for="postalCode">Postal Code</label>
          <input
            type="text"
            id="postalCode"
            v-model="customer.primaryAdress.postalCode"
          />
        </li>
        <li>
          <label for="country">Country</label>
          <input
            type="text"
            id="country"
            v-model="customer.primaryAdress.country"
          />
        </li>
        <li>
          <Field
            fieldName="example"
            :customValidator="validators.onlyLettersWithSpaces"
            @value-change="handleExample"
          />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <solar-button type="button" @click="save" aria-label="save new customer">
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

export default defineComponent({
  name: "NewCustomerModal",
  components: { SolarButton, SolarModal, Field },
  setup(_, ctx) {
    const customer = reactive({
      primaryAdress: {
        adressLine1: "",
        adressLine2: "",
        city: "",
        state: "",
        country: "",
        postalCode: "",
      },

      createdOn: new Date(),
      updatedOn: new Date(),
      firstName: "",
      lastName: "",
      id: 0,
    }) as ICustomer;
    const valid = ref(false);
    const handleExample = (value: any) => {
      console.log(value);
    };
    console.log("rerender");

    watch(
      () => customer.firstName,
      () => {
        console.log(
          "Firstname",
          validators.onlyLettersNoSpaces(customer.firstName)
        );
      }
    );

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
      handleExample,
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