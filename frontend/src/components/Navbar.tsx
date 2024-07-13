import "../style/Navbar.css";

function Navbar() {
  return (
      <nav className="navbar">
          <ul className="navbar-nav">
              <li className="nav-item">
                  <a href="#" className="nav-link">
                    <span className="link-text">dupa</span>
                  </a>
              </li>
              <li className="nav-item">
                  <a href="#" className="nav-link">
                      <span className="link-text">dupa2</span>
                  </a>
              </li>
          </ul>
      </nav>
  );
}

export default Navbar;