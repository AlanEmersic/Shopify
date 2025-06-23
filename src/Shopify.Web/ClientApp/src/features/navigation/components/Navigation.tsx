import { Link } from "react-router-dom";
import { useShallow } from "zustand/shallow";

import { NAVIGATION_ITEMS, ROUTES } from "features";
import { useAuthStore } from "stores";

function Navigation() {
  const { token, logout } = useAuthStore(useShallow(state => ({ token: state.token, logout: state.logout })));

  const handleOnLogoutClick = () => {
    logout();
  };

  return (
    <nav className="border-gray-200 bg-white pb-[50px]">
      <div className="mx-auto flex max-w-screen-xl flex-wrap items-center justify-between p-4">
        <div className="flex cursor-pointer items-center space-x-3 rtl:space-x-reverse">
          <Link
            to={ROUTES.HOME}
            className="block rounded px-3 py-2 text-gray-900 hover:bg-gray-100 md:p-0 md:hover:bg-transparent md:hover:text-cyan-700"
            aria-current="page"
          >
            <span className="self-center text-2xl font-semibold whitespace-nowrap">Shopify</span>
          </Link>
        </div>

        <div className="hidden w-full items-center justify-between md:order-1 md:flex md:w-auto">
          <div className="mt-4 rounded-lg border border-gray-100 bg-gray-50 font-medium md:mt-0 md:flex-row md:space-x-8 md:border-0 md:bg-white md:p-0 rtl:space-x-reverse">
            <div className="flex gap-10">
              {NAVIGATION_ITEMS.map(item => (
                <Link
                  key={item.id}
                  to={item.link}
                  className="block rounded px-3 py-2 text-gray-900 hover:bg-gray-100 md:hover:bg-transparent md:hover:text-cyan-700"
                  aria-current="page"
                >
                  {item.name}
                </Link>
              ))}

              {token ? (
                <>
                  <Link
                    to={ROUTES.PROFILE}
                    className="block rounded px-3 py-2 text-cyan-400 hover:bg-gray-100 md:hover:bg-transparent md:hover:text-cyan-700"
                    aria-current="page"
                  >
                    Profile
                  </Link>

                  <Link
                    onClick={handleOnLogoutClick}
                    to={ROUTES.LOG_OUT}
                    className="block rounded px-3 py-2 text-gray-900 hover:bg-gray-100 md:hover:bg-transparent md:hover:text-cyan-700"
                    aria-current="page"
                  >
                    Log Out
                  </Link>
                </>
              ) : (
                <>
                  <Link
                    to={ROUTES.LOG_IN}
                    className="block rounded px-3 py-2 text-gray-900 hover:bg-gray-100 md:hover:bg-transparent md:hover:text-cyan-700"
                    aria-current="page"
                  >
                    Log In
                  </Link>

                  <Link
                    to={ROUTES.REGISTER}
                    className="rounded-md bg-cyan-100 px-2 py-1 text-center text-xl text-cyan-400 md:hover:bg-cyan-200"
                    aria-current="page"
                  >
                    Register
                  </Link>
                </>
              )}
            </div>
          </div>
        </div>
      </div>
    </nav>
  );
}

export { Navigation };
