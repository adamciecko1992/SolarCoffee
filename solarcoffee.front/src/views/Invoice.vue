<template>
  <div>
    <h1 id="invoiceTitle">Create Invoice</h1>
    <hr />
    <div class="invoice-step" v-if="invoiceStep === 1">
      <h2>Step 1: Select Customer</h2>
      <div v-if="customers.length" class="invoice-step-detail">
        <label for="customer">Customer:</label>
        <select
          v-model="selectedCustomerId"
          class="invoice-customers"
          id="customer"
        >
          <option disabled value="">Please select a Customer</option>
          <option v-for="c in customers" :value="c.id" :key="c.id">
            {{ c.firstName + " " + c.lastName }}
          </option>
        </select>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep === 2">
      <h2>Step 2: Create Order</h2>

      <div v-if="inventory.length" class="invoice-step-detail">
        <label for="product">Product:</label>
        <select v-model="newItem.product" class="invoiceLineItem" id="product">
          <option disabled value="">Please select a Product</option>
          <option v-for="i in inventory" :value="i.product" :key="i.product.id">
            {{ i.product.name }}
          </option>
        </select>
        <label for="quantity">Quantity:</label>
        <input v-model="newItem.quantity" id="quantity" type="number" min="0" />
      </div>

      <div class="invoice-item-actions">
        <solar-button
          :disabled="!newItem.product || !newItem.quantity"
          @click="addLineItem"
        >
          Add Line Item
        </solar-button>

        <solar-button :disabled="!lineItems.length" @click="finalizeOrder">
          Finalize Order
        </solar-button>
      </div>

      <div class="invoice-order-list" v-if="lineItems.length">
        <div class="runningTotal">
          <h3>Running Total:</h3>
          {{ runningTotal }}
        </div>
        <hr />
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
            <td>{{ lineItem.product.price * lineItem.quantity }}</td>
          </tr>
        </table>
      </div>
    </div>

    <div class="invoice-step" v-if="invoiceStep === 3">
      <h2>Step 3: Review and Submit</h2>
      <solar-button @click="submitInvoice">Submit Invoice</solar-button>
      <hr />

      <div class="invoice-step-detail" id="invoice" ref="invoiceRef">
        <div class="invoice-logo">
          <img
            id="imgLogo"
            alt="Solar Coffee logo"
            src="../assets/images/solar_coffee_logo.png"
          />
          <h3>1337 Solar Lane</h3>
          <h3>San Somewhere, CA 90000</h3>
          <h3>USA</h3>

          <div class="invoice-order-list" v-if="lineItems.length">
            <div class="invoice-header">
              <h3>Invoice: {{ new Date() }}</h3>
              <hr />
              <section>
                <h3>
                  Customer:
                  {{
                    selectedCustomer.firstName + " " + selectedCustomer.lastName
                  }}
                </h3>
                <h3>
                  Address: {{ selectedCustomer.primaryAdress.adressLine1 }}
                </h3>
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
    <hr />

    <div class="invoice-steps-actions">
      <solar-button @click="prev" :disabled="!canGoPrev">Previous</solar-button>
      <solar-button @click="next" :disabled="!canGoNext">Next</solar-button>
      <solar-button @click="startOver">Start Over</solar-button>
    </div>
  </div>
</template>

<script lang="ts">
import CustomerService from "../services/customer-service";
import InventoryService from "../services/Inventory-service";
import InvoiceService from "../services/invoice-service";
import { ICustomer } from "../types/Customer";
import { IInvoice, ILineItem } from "../types/Invoice";
import { IProductInventory } from "../types/Product";
import { defineComponent, ref, reactive, computed, watch } from "vue";
import SolarButton from "../components/SolarButton.vue";
import jspdf from "jspdf";
import html2canvas from "html2canvas";
import { useRouter } from "vue-router";
import isValidResponse from "../helpers/axiosTypeGuard";

export default defineComponent({
  components: {
    SolarButton,
  },
  setup() {
    const router = useRouter();
    //
    const inventory = ref<IProductInventory[]>([]);
    const lineItems = ref<ILineItem[]>([]);
    const newItem = ref<ILineItem>({
      product: {
        id: 0,
        name: "",
        createdOn: new Date(),
        updatedOn: new Date(),
        description: "",
        price: 0,
        isTaxable: false,
        isArchived: false,
      },
      quantity: 0,
    });
    const inventoryService = new InventoryService();

    //Customers
    const customers = ref<ICustomer[]>([]);
    const customerService = new CustomerService();
    const selectedCustomerId = ref<number>(0);
    const selectedCustomer = reactive<ICustomer>({
      id: 0,
      createdOn: new Date(),
      updatedOn: new Date(),
      firstName: "",
      lastName: "",
      primaryAdress: {
        createdOn: new Date(),
        updatedOn: new Date(),
        id: 0,
        adressLine1: "",
        adressLine2: "",
        city: "",
        state: "",
        postalCode: "",
        country: "",
      },
    });
    watch(selectedCustomerId, async () => {
      const selected = await customerService.getCustomerById(
        selectedCustomerId.value
      );
      if (isValidResponse(selected)) {
        Object.assign(selectedCustomer, selected.data);
      }
    });
    //Invoice
    const invoiceStep = ref(1);
    const canGoNext = computed(() => {
      return invoiceStep.value === 3 || selectedCustomerId.value === 0
        ? false
        : true;
    });
    const canGoPrev = computed(() => {
      return invoiceStep.value === 1 ? false : true;
    });

    const runningTotal = computed(() => {
      return lineItems.value.reduce(
        (a, b) => a + b.product.price * b.quantity,
        0
      );
    });
    function next() {
      invoiceStep.value++;
    }
    function prev() {
      invoiceStep.value--;
    }
    function startOver() {
      invoiceStep.value = 0;
    }
    function addLineItem() {
      const lineItem: ILineItem = {
        product: newItem.value.product,
        quantity: newItem.value.quantity,
      };
      lineItems.value.push(lineItem);
    }
    function finalizeOrder() {
      invoiceStep.value = 3;
    }
    //Invoice
    const invoiceService = new InvoiceService();
    const invoiceRef = ref<HTMLElement | null>(null);

    function downloadPdf() {
      const pdf = new jspdf("p", "pt", "a4", true);
      const invoice = document.getElementById("invoice");
      if (invoiceRef.value) {
        const width = invoiceRef.value.clientWidth;
        const height = invoiceRef.value.clientHeight;

        if (invoice) {
          html2canvas(invoice).then((canvas) => {
            const image = canvas.toDataURL("image/png");
            pdf.addImage(image, "PNG", 0, 0, width * 0.55, height * 0.55);
            pdf.save("invoice");
          });
        }
      }
    }

    async function submitInvoice(): Promise<void> {
      const invoice: IInvoice = {
        customerId: selectedCustomerId.value,
        lineItems: lineItems.value,
        createdOn: new Date(),
        updatedOn: new Date(),
      };
      const response = await invoiceService.makeNewInvoice(invoice);

      if (isValidResponse(response)) {
        downloadPdf();
        router.push("/orders");
      }
    }

    const invoice = reactive({
      customerId: NaN,
      lineItems: [],
      createdOn: new Date(),
      updatedOn: new Date(),
    }) as IInvoice;

    //lifecycle

    async function initialize() {
      const response = await customerService.getCustomers();
      if (isValidResponse(response)) {
        customers.value = response.data;
      }
      const inventoryResponse = await inventoryService.getInventory();
      if (isValidResponse(inventoryResponse)) {
        inventory.value = inventoryResponse.data;
      }
    }

    initialize();

    return {
      invoiceStep,
      inventory,
      lineItems,
      newItem,
      customers,
      selectedCustomerId,
      invoice,
      selectedCustomer,
      next,
      prev,
      startOver,
      canGoPrev,
      canGoNext,
      runningTotal,
      addLineItem,
      submitInvoice,
      finalizeOrder,
      invoiceRef,
    };
  },
});
</script>

<style lang="scss">
@import "@/scss/global.scss";
.invoice-steps-actions {
  display: flex;
  width: 100%;
}
.invoice-step {
  color: inherit;
}
.invoice-step-detail {
  margin: 1.2rem;
}
.invoice-order-list {
  margin-top: 1.2rem;
  padding: 0.8rem;
  .line-item {
    display: flex;
    border-bottom: 1px dashed #ccc;
    padding: 0.8rem;
  }
  .item-col {
    flex-grow: 1;
  }
}
.invoice-item-actions {
  display: flex;
}
.price-pre-tax {
  font-weight: bold;
}
.price-final {
  font-weight: bold;
  color: $solar-green;
}
.due {
  font-weight: bold;
}
.invoice-header {
  width: 100%;
  margin-bottom: 1.2rem;
}
.invoice-logo {
  padding-top: 1.4rem;
  text-align: center;
  img {
    width: 280px;
  }
}
</style>