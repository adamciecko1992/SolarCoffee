<template>
  <div>
    <customer-selection
      v-if="invoiceStep === 1"
      :customers="customers"
      @customer-selected="handleCustomerSelection"
    ></customer-selection>
    <create-order
      v-if="invoiceStep === 2"
      @add-order="handleAddOrder"
      :inventory="inventory"
    ></create-order>
    <finalize-order
      v-if="invoiceStep === 3"
      :selectedCustomer="selectedCustomer"
      :lineItems="lineItems"
      @submit-invoice="handleSubmit"
    ></finalize-order>
    <hr />

    <div class="invoice-steps-actions">
      <solar-button @click="prev" :disabled="!canGoPrev">Previous</solar-button>
      <solar-button @click="next" :disabled="!canGoNext">Next</solar-button>
      <solar-button @click="startOver">Start Over</solar-button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, reactive, computed, watch } from "vue";
import CustomerService from "../../services/customer-service";
import InventoryService from "../../services/Inventory-service";
import InvoiceService from "../../services/invoice-service";
import { ICustomer } from "../../types/Customer";
import { IInvoice, ILineItem } from "../../types/Invoice";
import { IProductInventory } from "../../types/Product";
import isValidResponse from "../../helpers/axiosTypeGuard";
import SolarButton from "../../components/SolarButton.vue";
import CustomerSelection from "./CustomerSelection.vue";
import CreateOrder from "./CreateOrder.vue";
import FinalizeOrder from "./FinalizeOrder.vue";

import jspdf from "jspdf";
import html2canvas from "html2canvas";
import { useRouter } from "vue-router";

export default defineComponent({
  components: {
    SolarButton,
    CustomerSelection,
    CreateOrder,
    FinalizeOrder,
  },
  setup() {
    const router = useRouter();
    //
    const inventory = ref<IProductInventory[]>([]);
    const lineItems = ref<ILineItem[]>([]);
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
    function handleCustomerSelection(id: number) {
      selectedCustomerId.value = id;
    }
    watch(selectedCustomerId, async () => {
      const selected = await customerService.getCustomerById(
        selectedCustomerId.value
      );
      if (isValidResponse(selected)) {
        Object.assign(selectedCustomer, selected.data);
      }
    });

    //Invoice steps
    const invoiceStep = ref(1);
    const canGoNext = computed(() => {
      return invoiceStep.value === 3 || selectedCustomerId.value === 0
        ? false
        : true;
    });
    const canGoPrev = computed(() => {
      return invoiceStep.value === 1 ? false : true;
    });

    function next() {
      invoiceStep.value++;
    }
    function prev() {
      invoiceStep.value--;
    }
    function startOver() {
      invoiceStep.value = 1;
    }

    //Invoice generation

    const invoiceService = new InvoiceService();
    const invoiceRef = ref<HTMLElement | null>(null);
    const runningTotal = ref(0);

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

    function handleAddOrder(order: { items: ILineItem[]; total: number }) {
      lineItems.value = order.items;
      runningTotal.value = order.total;
      invoiceStep.value = 3;
    }

    async function handleSubmit(): Promise<void> {
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
      customers,
      selectedCustomer,
      next,
      prev,
      startOver,
      canGoPrev,
      canGoNext,
      handleSubmit,
      invoiceRef,
      handleCustomerSelection,
      handleAddOrder,
      runningTotal,
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