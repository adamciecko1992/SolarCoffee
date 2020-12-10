<template>
  <solar-modal>
    <template v-slot:header> Add New Customer </template>
    <template v-slot:body>
      <form class="newCustomer" @submit.prevent="save">
        <li>
          <Field
            fieldName="Firstname"
            @value-changed="handleCustomerPropChange($event, 'firstName')"
            :validationState="validationState.firstName"
          />
        </li>
        <li>
          <Field
            fieldName="Lastname"
            @value-changed="handleCustomerPropChange($event, 'lastName')"
            :validationState="validationState.lastName"
          />
        </li>
        <li>
          <Field
            fieldName="Adress"
            @value-changed="handleAdressChange($event, 'adressLine1')"
            :validationState="validationState.primaryAdressAdressLine1"
          />
        </li>
        <li>
          <Field
            fieldName="Adress line 2 "
            @value-changed="handleAdressChange($event, 'adressLine2')"
            :validationState="validationState.primaryAdressAdressLine2"
          />
        </li>
        <li>
          <Field
            fieldName="State"
            @value-changed="handleAdressChange($event, 'state')"
            :validationState="validationState.primaryAdressState"
          />
        </li>
        <li>
          <Field
            fieldName="Postal code"
            @value-changed="handleAdressChange($event, 'postalCode')"
            :validationState="validationState.primaryAdressPostalCode"
          />
        </li>
        <p v-if="unknownServerError !== ''">
          {{ unknownServerError }}
        </p>
      </form>
    </template>
    <template v-slot:footer>
      <solar-button type="button" aria-label="save new customer" @click="save">
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
import SolarButton from "../../components/SolarButton.vue";
import SolarModal from "../../components/modals/SolarModal.vue";
import Field from "../../components/Ui/Field.vue";
import { ICustomer } from "../../types/Customer";
import { ICustomerAdress } from "../../types/Customer";
import customerService from "../../services/customer-service";
import CustomerService from "../../services/customer-service";
import isValidResponse from "../../helpers/axiosTypeGuard";
import firstToLower from "../../helpers/firstToLower";
import firstToUpper from "../../helpers/firstToUpper";
import { AxiosError } from "axios";

export default defineComponent({
  name: "NewCustomerModal",
  components: { SolarButton, SolarModal, Field },
  emits: ["customer-added", "close"],
  setup(_, ctx) {
    //form part
    const customer: ICustomer = reactive({
      firstName: "",
      lastName: "",
      id: 0,
      createdOn: new Date(),
      updatedOn: new Date(),
      primaryAdress: {
        adressLine1: "",
        adressLine2: "",
        state: "",
        postalCode: "",
      },
    });

    const validationState = reactive({
      firstName: { valid: false, messages: [] },
      lastName: { valid: false, messages: [] },
      primaryAdressAdressLine1: { valid: false, messages: [] },
      primaryAdressAdressLine2: { valid: false, messages: [] },
      primaryAdressState: { valid: false, messages: [] },
      primaryAdressPostalCode: { valid: false, messages: [] },
    });

    function handleCustomerPropChange(value: string, field: keyof ICustomer) {
      (customer as any)[field] = value;
    }

    function handleAdressChange(value: unknown, field: keyof ICustomerAdress) {
      (customer as any).primaryAdress[field] = value;
    }

    //submit/errors part
    const customerService = new CustomerService();
    const unknownServerError = ref("");

    function handleSubmitError(error: AxiosError) {
      const errorStatus = error.response?.status;
      const errors = error.response?.data.errors;

      switch (errorStatus) {
        case 422:
          (() => {
            const errorsNames = Object.keys(errors).map((errorName) =>
              firstToLower(errorName)
            );
            errorsNames.forEach((errorName) => {
              if (errorName in validationState) {
                validationState[
                  errorName as keyof typeof validationState
                ].valid = false;
                validationState[
                  errorName as keyof typeof validationState
                ].messages = errors[firstToUpper(errorName)];
              }
            });
          })();
          break;
        //maybe some other cases in the future
        default:
          unknownServerError.value =
            "Unknown server error, if error will occure again please contact our support";
      }
    }

    async function save() {
      const response = await customerService.addCustomer(customer);
      if (isValidResponse(response)) {
        ctx.emit("customer-added");
      } else {
        handleSubmitError(response);
      }
    }

    function close() {
      ctx.emit("close");
    }
    return {
      save,
      close,
      customer,
      handleCustomerPropChange,
      handleAdressChange,
      validationState,
      unknownServerError,
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