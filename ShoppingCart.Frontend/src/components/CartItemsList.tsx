import type { CartItem } from "../types/CartItem"
import CartItemListElement from "./CartItemListElement";
import './CartItemsList.css';

type Props = {
  cartItems: CartItem[];
  onDelete: (articleId: number) => void;
};

export default function CartItemList({
  cartItems,
  onDelete
}: Props) {
  return (
    <>
      {cartItems.map(ci => (
        <CartItemListElement
          key={ci.articleId}
          cartItem={ci}
          onDelete={onDelete}
        />
      ))}
    </>
  );
}
