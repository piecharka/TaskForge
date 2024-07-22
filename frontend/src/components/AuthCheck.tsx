import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";
import { Navigate } from "react-router-dom";
import { ReactNode } from "react";

const AuthCheck = observer(({ children }: { children: ReactNode }) => {
    const { userStore } = useStore();

    return (
        <>
            {userStore.isLoggedIn ? children : <Navigate to="/login" /> }
        </>
    );
});

export default AuthCheck;