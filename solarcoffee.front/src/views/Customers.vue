<template>
  <div>
    <h1 id="customersTitle">Manage Customers</h1>
    <hr />
    <div class="customer-actions">
      <solar-button @click="showNewCustomerModal"> Add Customer </solar-button>
    </div>
    <table id="customers" class="table">
      <tr>
        <th>Customer</th>
        <th>Adress</th>
        <th>State</th>
        <th>Since</th>
        <th>Delete</th>
      </tr>
      <tr v-for="customer in customers" :key="customer.id">
        <td>
          {{ customer.firstName + " " + customer.lastName }}
        </td>
        <td>
          {{
            customer.primaryAdress?.adressLine1 +
            " " +
            customer.primaryAdress?.adressLine2
          }}
        </td>
        <td>
          {{ customer.primaryAdress?.state }}
        </td>
        <td>
          {{ customer.createdOn }}
        </td>
        <td>
          <solar-button @click="deleteCustomer(customer.id)"
            >Delete</solar-button
          >
        </td>
      </tr>
    </table>

    <new-customer-modal
      @close="closeModal"
      @save-customer="saveNewCustomer"
      v-if="isCustomerModalVisible"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, handleError, ref } from "vue";
import { ICustomer } from "../types/Customer";
import SolarButton from "@/components/SolarButton.vue";
import CustomerService from "../services/customer-service";
import NewCustomerModal from "@/components/modals/NewCustomerModal.vue";
import handleErrors from "../helpers/handleErrors";
const customerService = new CustomerService();

export default defineComponent({
  name: "Customers",
  components: { SolarButton, NewCustomerModal },

  setup() {
    const customers = ref<ICustomer[]>([]);
    const isCustomerModalVisible = ref(false);
    const showNewCustomerModal = (): void => {
      isCustomerModalVisible.value = true;
    };
    async function initialize() {
      const fetchedData = await customerService.getCustomers();
      //typeguard - no error
      if ("data" in fetchedData) {
        customers.value = fetchedData.data;
      }
    }

    const closeModal = (): void => {
      isCustomerModalVisible.value = false;
    };
    async function saveNewCustomer(newCustomer: ICustomer) {
      const response = await customerService.addCustomer(newCustomer);
      isCustomerModalVisible.value = false;
      await initialize();
    }
    async function deleteCustomer(id: number) {
      await customerService.deleteCustomer(id);
      await initialize();
    }

    async function created() {
      await initialize();
    }

    initialize();

    return {
      customers,
      showNewCustomerModal,
      closeModal,
      deleteCustomer,
      created,
      saveNewCustomer,
      isCustomerModalVisible,
    };
  },
});
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.customer-actions {
  display: flex;
  margin-bottom: 0.8rem;
  div {
    margin-right: 0.8rem;
  }
}
.customer-delete {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
