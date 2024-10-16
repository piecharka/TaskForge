import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { User } from "../models/user";

const UserDetailsView = observer(() => {
    const { userId } = useParams<{ userId: string }>();
    const [user, setUser] = useState<User>();

    useEffect(() => {
        apiHandler.Users.getUserById(Number(userId))
            .then(response => setUser(response));
    }, [userId])

    return (
        <div>
            { user?.username }
        </div>
  );
});

export default UserDetailsView;