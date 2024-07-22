import { useEffect, useState } from "react";
import { Team} from "../models/team";
import apiHandler from "../api/apiHandler";
import { useStore } from "../stores/store";
import { Link } from "react-router-dom";
import "../style/TeamList.css"

function TeamListView() {
    const [teams, setTeams] = useState<Team[]>([]);
    const { userStore } = useStore();

    useEffect(() => {
        apiHandler.Teams.teamsByUsername(userStore.user?.username)
            .then(response => setTeams(response));
    }, [userStore])

  return (
      <div className={`team-list ${teams.length<= 2 ? 'few-teams' : ''}`}>{teams.map(t => (
          <div key={t.teamId} className="team-box">
              <Link to={ "" + t.teamId} className="team-link">
                  <h2>{t.teamName}</h2>
                  <p>Users in team: {t.users.length}</p>
              </Link>
          </div>
      ))}
      </div>
  );
}

export default TeamListView;

//<Link to="team" className="nav-link">
//    <span className="link-text">Teams</span>
//</Link>