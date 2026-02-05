import type { CartItem } from "../types/CartItem"
import CartItemListElement from "./CartItemListElement";

type Props = {
  cartItems: CartItem[];
  onAdd: (id: number) => void;
  onDelete: (id: number) => void;
};

export default function CartItemList({
  cartItems,
  onAdd,
  onDelete
}: Props) {
  return (
   <div>
      {cartItems.map(ci => (
        <CartItemListElement
          key={ci.articleId}
          cartItem={ci}
          onAdd={onAdd}
          onDelete={onDelete}
        />
      ))}
    </div>
  );
}