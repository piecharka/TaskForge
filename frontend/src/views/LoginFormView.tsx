import { useState } from "react";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import { Link, useNavigate } from "react-router-dom";
import "../style/LoginForm.css";

const LoginFormView = observer(function LoginFormView() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string | null>(null);
    const { userStore } = useStore();
    const navigate = useNavigate();

    const handleSubmit = (e : React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        userStore.login({ username, password })
        .then(() => {
            setError('');
            navigate('/');
        }).catch((error: any) => {
            if (error.response && error.response.data && error.response.data.message) {
                setError(error.response.data.message);
            } else if (error.message) {
                setError(error.message);
            } else {
                setError('An unexpected error occurred');
            }
        })
        //navigate('/');
    }

  return (
      <div className="card-box">
          <div className="first-part">
              <div className="app-title">
                  <h1>Task Forge</h1>
                  <p>Organize and schedule your team's time</p>
              </div>
          </div>
          <div className="second-part">
              <div>
                  <h1>Login</h1>
                  {error && <div className="error">{error}</div>} {/* Wyœwietl komunikat b³êdu */}
                  <form onSubmit={handleSubmit}>
                      <div className="form-group">
                          <input
                              type="username"
                              id="username"
                              value={username}
                              onChange={(e) => setUsername(e.target.value)}
                              placeholder="username"
                          />
                      </div>
                      <div className="form-group">
                          <input
                              type="password"
                              id="password"
                              value={password}
                              onChange={(e) => setPassword(e.target.value)}
                              placeholder="password"
                          />
                      </div>
                      <button type="submit">Submit</button>
                  </form>

              </div>
                  <Link className="register-link" to="/register/" >
                    <span>Create your account &rarr;</span>
                  </Link>
          </div>
      </div>
  );
})

export default LoginFormView;