import type { CartItem } from "./CartItem";

export interface Cart {
    items: CartItem[];
    totalPrice: number;
}