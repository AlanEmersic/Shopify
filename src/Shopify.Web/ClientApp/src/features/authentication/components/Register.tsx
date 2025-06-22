import clsx from "clsx";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useShallow } from "zustand/shallow";

import { ROUTES, type ValidationErrors, useRegister } from "features";
import { useAuthStore } from "stores";

function Register() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [phone, setPhone] = useState("");
  const [address, setAddress] = useState("");
  const [error, setError] = useState("");
  const [validationErrors, setValidationErrors] = useState<ValidationErrors>();
  const registerMutation = useRegister();

  const navigate = useNavigate();

  const loginStore = useAuthStore(useShallow(state => state.login));

  const handleOnSubmitClick = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    registerMutation.mutate(
      {
        email: email,
        password: password,
        phone: phone,
        address: address
      },
      {
        onSuccess(data) {
          if (data) {
            loginStore(data.email, data.token);
            navigate(ROUTES.HOME);
          }
        },
        onError(error) {
          setError(error.response.data.title);
          setValidationErrors(error.response.data.errors);
        },
      },
    );
  };

  const handleOnLoginClick = () => {
    navigate(ROUTES.LOG_IN);
  };

  return (
    <section className="bg-gray-50">
      <div className="mx-auto flex flex-col items-center px-6 py-8 md:h-screen lg:py-0">
        <div className="w-full rounded-lg bg-white shadow sm:max-w-md md:mt-0 xl:p-0">
          <div className="space-y-4 p-6 sm:p-8 md:space-y-6">
            <h1 className="text-xl leading-tight font-bold tracking-tight text-gray-900 md:text-2xl">Create your account</h1>

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
                  className="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 sm:text-sm"
                  placeholder="Enter your email"
                  onChange={event => setEmail(event.target.value)}
                />
              </div>

              <div>
                <label htmlFor="password" className="mb-2 block text-sm font-medium text-gray-900">
                  Password
                </label>
                <p className="mb-2 text-sm">
                  Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character.
                </p>
                <input
                  type="password"
                  name="password"
                  id="password"
                  autoComplete="current-password"
                  placeholder="Enter your password"
                  className="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 sm:text-sm"
                  onChange={event => setPassword(event.target.value)}
                />
              </div>

              <div>
                <label htmlFor="phone" className="mb-2 block text-sm font-medium text-gray-900">
                  Phone
                </label>
                <input
                  type="text"
                  name="phone"
                  id="phone"
                  placeholder="Enter your phone number"
                  className="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 sm:text-sm"
                  onChange={event => setPhone(event.target.value)}
                />
              </div>

              <div>
                <label htmlFor="address" className="mb-2 block text-sm font-medium text-gray-900">
                  Address
                </label>
                <input
                  type="text"
                  name="address"
                  id="address"
                  placeholder="Enter your address"
                  className="block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 sm:text-sm"
                  onChange={event => setAddress(event.target.value)}
                />
              </div>

              <button
                type="submit"
                className={clsx(
                  "w-full rounded-lg bg-gray-400 px-5 py-2.5 text-center text-lg font-medium text-white",
                  !registerMutation.isPending && email && password && phone && address && "hover:bg-cyan-500",
                )}
                disabled={registerMutation.isPending || !email || !password || !phone || !address}
              >
                Create an account
              </button>
              <p className="text-sm font-light text-gray-500">
                Already have an account?{" "}
                <button onClick={handleOnLoginClick} className="font-medium text-gray-600 hover:cursor-pointer hover:text-cyan-500">
                  Login here
                </button>
              </p>
            </form>

            {registerMutation.error instanceof Error && (
              <p className="text-red-500">
                {error}
                {validationErrors && (
                  <div>
                    {Object.entries(validationErrors).map(([key, value]) => (
                      <p key={key}>{value}</p>
                    ))}
                  </div>
                )}
              </p>
            )}
          </div>
        </div>
      </div>
    </section>
  );
}

export { Register };
