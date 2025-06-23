import { AddressIcon, EmailIcon } from "assets";
import type { User } from "features";

type UserProfileDetailsProps = {
  user: User;
};

function UserProfileDetails({ user }: Readonly<UserProfileDetailsProps>) {

  if (!user) {
    return <div className="m-auto text-center text-2xl">User not found</div>;
  }

  return (
    <div className="text-lg">
      <h2 className="text-2xl font-semibold">User Details</h2>
      <p className="flex flex-row items-center gap-1">
        <EmailIcon className="h-6 w-6 fill-slate-100" />
        <span className="font-semibold">Email:</span> <span className="text-cyan-400">{user.email}</span>
      </p>
      <p className="flex flex-row items-center gap-1">
        <AddressIcon className="h-6 w-6 fill-slate-500" />
        <span className="font-semibold">Address:</span> <span className="text-cyan-400">{user.address}</span>
      </p>
    </div>
  );
}

export { UserProfileDetails };
