import clsx from "clsx";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useShallow } from "zustand/shallow";

import { ROUTES, useLogin } from "features";
import { useAuthStore } from "stores";

function Login() {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string>("");
  const navigate = useNavigate();

  const loginMutation = useLogin();
  const loginStore = useAuthStore(useShallow(state => state.login));

  const handleOnSubmitClick = (event: React.FormEvent) => {
    event.preventDefault();

    loginMutation.mutate(
      { email: email, password: password },
      {
        onSuccess(data) {
          if (data) {
            loginStore(data.email, data.token);
            navigate(ROUTES.HOME);
          }
        },
        onError(error) {
          setError(error.response.data.detail);
        },
      },
    );
  };

  const handleOnRegisterClick = () => {
    navigate(ROUTES.REGISTER);
  };

  return (
    <section className="bg-gray-50">
      <div className="mx-auto flex flex-col items-center px-6 py-8 md:h-screen lg:py-0">
        <div className="w-full rounded-lg bg-white shadow sm:max-w-md md:mt-0 xl:p-0">
          <div className="space-y-4 p-6 sm:p-8 md:space-y-6">
            <h1 className="text-xl leading-tight font-bold tracking-tight text-gray-900 md:text-2xl">Log in to your account</h1>

            <form className="space-y-4 md:space-y-6" onSubmit={handleOnSubmitClick}>
              <div>
                <label htmlFor="email" className="mb-2 block text-sm font-medium text-gray-900">
                  Your email
                </label>
                <input
                  type="email"
                  name="email"
                  id="email"
                  autoComplete="email"
                  className="focus:ring-primary-600 focus:border-primary-600 block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900"
                  placeholder="Enter your email"
                  onChange={event => setEmail(event.target.value)}
                />
              </div>
              <div>
                <label htmlFor="password" className="mb-2 block text-sm font-medium text-gray-900">
                  Password
                </label>
                <input
                  type="password"
                  name="password"
                  id="password"
                  autoComplete="current-password"
                  placeholder="Enter your password"
                  className="focus:ring-primary-600 focus:border-primary-600 block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900"
                  onChange={event => setPassword(event.target.value)}
                />
              </div>

              <button
                type="submit"
                className={clsx(
                  "w-full rounded-lg px-5 py-2.5 text-center text-lg font-medium text-white",
                  !loginMutation.isPending && email && password && "bg-cyan-500",
                  !loginMutation.isPending && (!email || !password) && "bg-gray-400",
                )}
                disabled={loginMutation.isPending || !email || !password}
              >
                Log in
              </button>

              <p className="text-base font-medium text-gray-500">
                Don’t have an account yet?{" "}
                <button onClick={handleOnRegisterClick} className="font-medium text-gray-600 hover:cursor-pointer hover:text-cyan-500">
                  Register
                </button>
              </p>
            </form>

            {loginMutation.error instanceof Error && <p className="text-red-500">{error}</p>}
          </div>
        </div>
      </div>
    </section>
  );
}

export { Login };

