const { createYield } = require("typescript")

context('SideMenu', () => {
    beforeEach(() => {
        cy.visit("http://localhost:8080");
    })
    it("visits inventory page via logo", () => {
        cy.get("#logo").click();
        cy.get("#inventoryTitile").contains("Inventory Dashboard");
    })
})