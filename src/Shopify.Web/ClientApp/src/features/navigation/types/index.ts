export const ROUTES = {
  HOME: "/",
  PRODUCT_DETAILS: "/product/:id",
  LOG_IN: "/login",
  LOG_OUT: "/logout",
  REGISTER: "/register",
  PROFILE: "/profile",
  CART: "/cart",
};

export type NavigationItem = {
  id?: number;
  name: "Home";
  link: string;
};

export const NAVIGATION_ITEMS: NavigationItem[] = [
  {
    id: 1,
    name: "Home",
    link: ROUTES.HOME,
  },
];