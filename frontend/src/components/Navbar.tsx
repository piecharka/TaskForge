import { Link } from "react-router-dom";
import "../style/Navbar.css";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";

const Navbar = observer(() => {
    const { userStore } = useStore();

    const handleLogout = () => {
        userStore.logout();
    }

  return (
      <nav className="navbar">
          <ul className="navbar-nav">
              { userStore.isLoggedIn ? <li className="nav-item">
                  <Link  to="/login" onClick={handleLogout} className="nav-link">
                    <span className="link-text">Log out</span>
                  </Link>
              </li> : <li className="nav-item">
                  <Link to="login" className="nav-link">
                      <span className="link-text">Log in</span>
                  </Link>
              </li>}
              <li className="nav-item">
                  <Link to="#" className="nav-link">
                      <span className="link-text">dupa2</span>
                  </Link>
              </li>
          </ul>
      </nav>
  );
})

export default Navbar;