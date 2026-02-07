import { createContext, useContext, useState } from "react";

type CartContextType = {
  invalidItems: number[];
  setInvalidItems: (ids: number[]) => void;
};

const CartContext = createContext<CartContextType | undefined>(undefined);

export function CartProvider({ children }: { children: React.ReactNode }) {
  const [invalidItems, setInvalidItems] = useState<number[]>([]);

  return (
    <CartContext.Provider value={{ invalidItems, setInvalidItems }}>
      {children}
    </CartContext.Provider>
  );
}

export function useCartContext() {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error("useCartContext must be used inside CartProvider");
  }
  return context;
}
