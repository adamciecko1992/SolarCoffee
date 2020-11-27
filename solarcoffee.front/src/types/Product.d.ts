//.d.ts signals that this is a typescript declaration file
export interface IProduct {
  id: number;
  name: string;
  createdOn: Date;
  updatedOn: Date;
  description: string;
  price: number;
  isTaxable: boolean;
  isArchived: boolean;

  //   Id: number;
  //   CreatedOn: Date;
  //   UpdatedOn: Date;
  //   Name: string;
  //   Description: string;
  //   Price: number;
  //   IsArchived: boolean;
  //   IsTaxable: boolean;
}

export interface IProductInventory {
  id: number;
  product: IProduct;
  quantityOnHand: number;
  idealQuantity: number;
}
