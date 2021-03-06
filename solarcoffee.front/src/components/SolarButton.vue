<template>
  <button
    v-if="link"
    @click="visitRoute"
    :class="['solar-button', { 'full-width': props.isFullWidth }]"
  >
    <slot></slot>
  </button>
  <button
    v-else
    :class="['solar-button', { 'full-width': props.isFullWidth }]"
    @click="handleClick"
    :disabled="isDisabled"
  >
    <slot></slot>
  </button>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
export default defineComponent({
  name: "solarButton",
  props: {
    link: { type: String },
    isFullWidth: { type: Boolean },
    isDisabled: { type: Boolean },
  },

  setup(props, ctx) {
    const router = useRouter();
    const visitRoute = () => props.link && router.push(props.link);
    const handleClick = () => ctx.emit("btn-clicked");
    return {
      visitRoute,
      handleClick,
      props,
    };
  },
});
</script>

<style lang="scss">
@import "@/scss/global.scss";
.solar-button {
  background: lighten($solar-blue, 10%);
  color: white;
  padding: 0.8rem;
  transition: background-color 0.5s;
  margin: 0.3rem 0.2rem;
  display: inline-block;
  cursor: pointer;
  font-size: 1rem;
  min-width: 100px;
  border: none;
  border-bottom: 2px solid darken($solar-blue, 20%);
  border-radius: 3px;
  &:hover {
    background: lighten($solar-blue, 20%);
    transition: background-color 0.5s;
  }
  &:disabled {
    background: lighten($solar-blue, 15%);
    border-bottom: 2px solid lighten($solar-blue, 20%);
  }
  &:active {
    background: $solar-yellow;
    border-bottom: 2px solid lighten($solar-yellow, 20%);
  }
}
.full-width {
  display: block;
  width: 100%;
}
</style>