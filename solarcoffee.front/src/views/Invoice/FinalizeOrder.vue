<template>
  <div class="invoice-step">
    <h2>Step 3: Review and Submit</h2>
    <solar-button @click="submitInvoice">Submit Invoice</solar-button>
    <hr />

    <div class="invoice-step-detail" id="invoice" ref="invoiceRef">
      <div class="invoice-logo">
        <img
          id="imgLogo"
          alt="Solar Coffee logo"
          src="../../assets/images/solar_coffee_logo.png"
        />
        <h3>1337 Solar Lane</h3>
        <h3>San Somewhere, CA 90000</h3>
        <h3>USA</h3>

        <div class="invoice-order-list" v-if="lineItems.length">
          <div class="invoice-header">
            <h3>Invoice: {{ new Date().getDate() }}</h3>
            <hr />
            <section>
              <h3>
                Customer:
                {{
                  selectedCustomer.firstName + " " + selectedCustomer.lastName
                }}
              </h3>
              <h3>Address: {{ selectedCustomer.primaryAdress.adressLine1 }}</h3>
              <h3 v-if="selectedCustomer.primaryAdress.adressLine2">
                {{ selectedCustomer.primaryAdress.adressLine2 }}
              </h3>
              <h3>
                City:
                {{ selectedCustomer.primaryAdress.state }}, Postal Code
                {{ selectedCustomer.primaryAdress.postalCode }}
              </h3>
            </section>
          </div>
          <table class="table">
            <thead>
              <tr>
                <th>Product</th>
                <th>Description</th>
                <th>Qty.</th>
                <th>Price</th>
                <th>Subtotal</th>
              </tr>
            </thead>
            <tr
              v-for="lineItem in lineItems"
              :key="`index_${lineItem.product.id}`"
            >
              <td>{{ lineItem.product.name }}</td>
              <td>{{ lineItem.product.description }}</td>
              <td>{{ lineItem.quantity }}</td>
              <td>{{ lineItem.product.price }}</td>
              <td>
                {{ lineItem.product.price * lineItem.quantity }}
              </td>
            </tr>
            <tr>
              <th colspan="4"></th>
              <th>Grand Total</th>
            </tr>
            <tfoot>
              <tr>
                <td colspan="4" class="due">Balance due upon receipt:</td>
                <td class="price-final">{{ runningTotal }}</td>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType, ref, watch } from "vue";
import { ICustomer } from "../../types/Customer";
import { ILineItem } from "../../types/Invoice";
import SolarButton from "../../components/SolarButton.vue";

export default defineComponent({
  components: {
    SolarButton,
  },
  props: {
    selectedCustomer: {
      type: Object as () => ICustomer,
      required: true,
    },
    lineItems: {
      type: Array as () => ILineItem[],
      required: true,
    },
    runningTotal: {
      type: Number,
      required: true,
    },
  },
  emits: ["submit-invoice"],

  methods: {
    submitInvoice() {
      this.$emit("submit-invoice");
    },
  },
});
</script>

<style lang="scss">
</style>