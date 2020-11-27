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
          <label for="productName">Name</label>
          <input type="text" id="productName" v-model="newProduct.name" />
        </li>

        <li>
          <label for="productDesc">Description</label>
          <input
            type="text"
            id="productDesc"
            v-model="newProduct.description"
          />
        </li>

        <li>
          <label for="productPrice">Price (USD)</label>
          <input type="number" id="productPrice" v-model="newProduct.price" />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <solar-button type="button" @click="save" aria-label="save new item">
        Save Product
      </solar-button>
      <solar-button type="button" @click="close" aria-label="close modal">
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";
import SolarButton from "@/components/SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue";
// import { IProduct } from "@/types/Product";

export default defineComponent({
  components: {
    SolarButton,
    SolarModal,
  },
  emits: ["save-product", "close"],

  setup(_, ctx) {
    const newProduct = reactive({
      createdOn: new Date(),
      updatedOn: new Date(),
      id: 0,
      description: "",
      isTaxable: false,
      name: "",
      price: 0,
      isArchived: false,
    });
    function close() {
      ctx.emit("close");
    }
    function save() {
      ctx.emit("save-product", newProduct);
    }

    return { save, close, newProduct };
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