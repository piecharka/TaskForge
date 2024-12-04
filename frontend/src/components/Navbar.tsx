import { Link } from "react-router-dom";
import "../style/Navbar.css";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import { Team } from "../models/team";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";

const Navbar = observer(() => {
    const [showTeams, setShowTeams] = useState<boolean>(false);
    const [teams, setTeams] = useState<Team[]>([]);
    const { userStore } = useStore();

    useEffect(() => {
        apiHandler.Teams.teamsByUsername(userStore.user?.username)
            .then(response => setTeams(response));
    }, [userStore])

    const handleLogout = () => {
        userStore.logout();
    }

    const teamOnClick = () => {
        setShowTeams(!showTeams);
        console.log(teams)
        console.log(showTeams)
    }

  return (
      <nav className="navbar">
          {
              userStore.isLoggedIn && <div>
          {!showTeams && 
          <ul className="navbar-nav">
                  
              {userStore.isLoggedIn && 
                  <li className="nav-item">
                      <Link to="/" className="nav-link">
                        <span className="link-text">Dashboard</span>
                      </Link>
                  </li>
              }
              {userStore.isLoggedIn && 
                  <li className="nav-item">
                      <Link to="/calendar" className="nav-link">
                        <span className="link-text">Calendar</span>
                      </Link>
                  </li>
              }
                  {userStore.isLoggedIn && 
                  <li className="nav-item">
                      <Link to="/notifications" className="nav-link">
                        <span className="link-text">Notifications</span>
                      </Link>
                  </li>}

                  <div className="nav-team"><li className="nav-item">
                  <Link to="#" className="nav-link" onClick={teamOnClick}>
                          <span className="link-text">Teams</span>
                  </Link>

              
                  
                </li>
              </div>
           

              <li className="nav-item">
                  <Link to="/login" onClick={handleLogout} className="nav-link">
                      <span className="link-text">Log out</span>
                  </Link>
              </li>
          </ul>
          }
          {showTeams && <ul className="navbar-nav">
              <li className="nav-item">
                  <Link to="#" onClick={teamOnClick} className="nav-link">
                      <span className="link-text">Go back</span>
                  </Link>
              </li>
              {teams.map(t => (
                  <li className="nav-item">
                      <Link to={"/team/" + t.teamId} className="nav-link">
                          <span className="link-text">{t.teamName}</span>
                      </Link>
                  </li>))}
          </ul>}
              </div>}
          {!userStore.isLoggedIn &&
          <ul className="navbar-nav">
                  <li className="nav-item">
                      <Link to="login" className="nav-link">
                          <span className="link-text">Login</span>
                      </Link>
                  </li>
          </ul>}
      </nav>
  );
})

export default Navbar;