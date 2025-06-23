import clsx from "clsx";
import type { SetStateAction } from "react";

import { MealPlanIcon, UserCardIcon } from "assets";
import { USER_PROFILE_TABS, USER_PROFILE_TABS_ICONS } from "features";

type UserProfileTabsProps = {
  tabItems: typeof USER_PROFILE_TABS;
  activeTabItemId: string;
  setActiveTabItemId: (value: SetStateAction<string>) => void;
};

function UserProfileTabs({ tabItems, activeTabItemId, setActiveTabItemId }: Readonly<UserProfileTabsProps>) {
  return (
    <div className="relative">
      <div className={clsx("flex gap-10 border-b border-gray-300")}>
        {tabItems.map(item => (
          <button
            key={item.id}
            className={clsx("group relative border-b-2 border-transparent pb-2 hover:cursor-pointer")}
            onClick={() => setActiveTabItemId(item.id)}
            onKeyDown={() => setActiveTabItemId(item.id)}
            tabIndex={0}
          >
            <p
              className={clsx(
                "text-System/Text/Tertiary text-lg leading-5 font-semibold whitespace-nowrap",
                activeTabItemId === item.id && "text-cyan-500",
                activeTabItemId !== item.id && "group-hover:text-gray-500",
              )}
            >
              {item.icon === USER_PROFILE_TABS_ICONS["user-details"] && <UserCardIcon className="mr-2 inline-block h-6 w-6 fill-slate-100" />}
              {item.icon === USER_PROFILE_TABS_ICONS["orders"] && <MealPlanIcon className="mr-2 inline-block h-6 w-6" />}

              {item.name}
            </p>
            {activeTabItemId === item.id && <div className="absolute bottom-[-3px] w-full rounded-xs border border-cyan-500"></div>}
          </button>
        ))}
      </div>
    </div>
  );
}

export { UserProfileTabs };
