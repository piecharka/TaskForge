import { useCallback, useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";
import { User } from "../models/user";
import { useParams } from "react-router-dom";
import { Permission } from "../models/permission";
import "../style/TeamSettings.css"

function TeamSettings() {
    const [teamUsers, setTeamUsers] = useState<User[]>([]);
    const [changePermissionView, setChangePermissionView] = useState(-1);
    const [permissionList, setPermissionList] = useState<Permission[]>([]);
    const [permissionId, setPermissionId] = useState(-1);
    const tableHeaders = ["Username", "Email", "Last login", "Permission", "Change permission", "Delete"]
    const { teamId } = useParams();

    const dateOptions = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
    };

    const fetchTeamUsers = useCallback(() => {
        apiHandler.Users.teamUsers(Number(teamId))
            .then(response => setTeamUsers(response))
            .catch(error => console.error("Failed to fetch team users: ", error));
    }, [teamId]);

    useEffect(() => {
        fetchTeamUsers();

        apiHandler.Permission.getPermissions()
            .then(response => setPermissionList(response));

    }, [fetchTeamUsers])

    const handleDeleting = (userId: number) => {

        apiHandler.Teams.deleteUserFromTeam(userId, Number(teamId))
            .then(() => {
                fetchTeamUsers();
            })
            .catch(error => console.error("Failed to delete user: ", error));
    }

    const handleChangingPermission = (userId: number) => {

            apiHandler.Permission.updateUsersPermission(userId, Number(teamId), permissionId)
            .then(() => {
                fetchTeamUsers();
                setChangePermissionView(-1);
            })

       
    }


  return (
    <div>
          <table className="task-table">
              <thead>
                  <tr>
                      {tableHeaders.map(header => (
                          <th key={header} className="header-cell">
                              {header}
                          </th>
                      ))}
                  </tr>
              </thead>
              <tbody>
                  {teamUsers && teamUsers.map((t) => (
                      <tr key={t.userId} className="task-row">
                          <td className="task-cell">{t.username}</td>
                          <td className="task-cell">{t.email}</td>
                          <td className="task-cell">{new Date(t.lastLogin).toLocaleDateString('en-EN', dateOptions)}</td>
                          {changePermissionView !== t.userId && <td className="task-cell">{t.permission.permissionName}</td>}
                          {changePermissionView === t.userId && <select id="permission"
                              name="permission"
                              value={permissionId}
                              onChange={(e) => {setPermissionId(e.target.value)} }>
                              {permissionList && permissionList.map(p => {
                                  return <option key={p.permissionName} value={p.permissionId}>{p.permissionName}</option>
                              })}
                          </select> }
                          <td className="task-cell">
                              {changePermissionView !== t.userId && <button onClick={() => {
                                  setChangePermissionView(t.userId);
                                  setPermissionId(1);
                              }}>Change permission</button>}
                              {changePermissionView === t.userId && <button onClick={() => handleChangingPermission(t.userId)}>Submit changes</button> }
                          </td>
                          <td className="task-cell">
                              <button className="delete-btn" onClick={() => handleDeleting(t.userId) }>Delete user</button>
                          </td>
                      </tr>
                  ))}
              </tbody>
          </table>
    </div>
  );
}

export default TeamSettings;