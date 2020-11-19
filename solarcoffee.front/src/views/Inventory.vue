<template>
  <div class="inventory-container">
    <h1 id="inventoryTitile">Inventory Dashboard</h1>
    <hr />
    <div class="inventory-actions">
      <solar-button @click="showInventoryItems">Inventory Items</solar-button>
      <solar-button @click="showShipment">Shipment</solar-button>
    </div>
    <table id="InventoryTable" class="table">
      <tr>
        <td>Item</td>
        <td>Quantity</td>
        <td>Unit Price</td>
        <td>Taxable</td>
        <td>Delete</td>
      </tr>
      <tr v-for="item in inventory" :key="item.id">
        <td>{{ item.product.name }}</td>
        <td>{{ item.quantityOnHand }}</td>
        <td>{{ parsePrice(item.product.price) }}</td>
        <td>
          <span v-if="item.product.isTaxable">Yes</span>
          <span v-else>No</span>
        </td>
        <td><solar-button isFullWidth>Delete</solar-button></td>
      </tr>
    </table>

    <new-product-modal
      v-if="newProductShown"
      @close="closeModals"
      @save-product="saveProduct"
    ></new-product-modal>
    <shipment-modal
      v-if="shipmentShown"
      :inventory="inventory"
      @close="closeModals"
      @save-shipment="saveShipment"
    ></shipment-modal>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { IProduct, IProductInventory } from "../types/Product";
import SolarButton from "../components/SolarButton.vue";
import newProductModal from "../components/modals/NewProductModal.vue";
import shipmentModal from "../components/modals/ShipmentModal.vue";
import { IShipment } from "@/types/Shipment";
export default defineComponent({
  name: "Inventory",
  components: {
    SolarButton,
    newProductModal,
    shipmentModal,
  },

  setup() {
    const newProductShown = ref(false);
    const shipmentShown = ref(false);

    const inventory = ref<IProductInventory[]>([
      {
        id: 1,
        quantityOnHand: 10,
        idealQuantity: 50,
        product: {
          id: 1,
          name: "darkCoffe",
          createdOn: new Date(),
          updatedOn: new Date(),
          description: "example description",
          price: 10,
          isTaxable: false,
          isArchived: true,
        },
      },
    ]);
    const parsePrice = (price: number) => {
      return price.toString() + "$";
    };
    const showInventoryItems = (): void => {
      newProductShown.value = !newProductShown.value;
    };

    const showShipment = (): void => {
      shipmentShown.value = !shipmentShown.value;
    };

    const closeModals = (): void => {
      newProductShown.value = false;
      shipmentShown.value = false;
    };

    const saveShipment = (shipment: IShipment) => {
      console.log(shipment);
    };

    const saveProduct = (product: IProduct) => {
      console.log(product);
    };

    return {
      inventory,
      parsePrice,
      showInventoryItems,
      newProductShown,
      shipmentShown,
      showShipment,
      closeModals,
      saveProduct,
      saveShipment,
    };
  },
});
</script>

<style lang="scss" scoped>
@import "../scss/global.scss";
.table {
  width: 100%;
  max-width: 100%;
  margin-bottom: 2rem;
  border-collapse: collapse;

  tr {
    border-top: 1px solid #eee;
    border-bottom: 1px solid #eee;
    transition: background-color 0.3s;

    &:hover {
      background-color: #f5f5f5;
      transition: background-color 0.3s;
    }
  }

  td {
    padding: 1.2rem;
  }

  th {
    background-color: #fafafa;
    padding: 1.2rem;
    border-bottom: 2px solid $solar-blue;
  }
}
</style>