import type { CartItem } from "../../types/CartItem"
import CartItemListElement from "./CartItemListElement";
import './CartItemsList.css';

type Props = {
  cartItems: CartItem[];
  invalidItems: number[];
  onChange: (aritcleId: number, quantity: number) => void
  onDelete: (articleId: number) => void;
};

export default function CartItemList({
  cartItems,
  invalidItems,
  onChange,
  onDelete
}: Props) {
  return (
    <>
      {cartItems.map(ci => (
        <CartItemListElement
          key={ci.id}
          cartItem={ci}
          isInvalid={invalidItems.includes(ci.id)}
          onChange={onChange}
          onDelete={onDelete}
        />
      ))}
    </>
  );
}
