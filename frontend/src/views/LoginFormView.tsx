import { useState } from "react";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router-dom";

const LoginFormView = observer(function LoginFormView() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const { userStore } = useStore();
    const navigate = useNavigate();

    const handleSubmit = () => {
        userStore.login({ username, password })
        navigate('/');
    }

  return (
      <div className="login-form">
          <form onSubmit={ handleSubmit }>
              <div className="form-group">
                  <label htmlFor="username">Username</label>
                  <input
                      type="username"
                      id="username"
                      value={username}
                      onChange={(e) => setUsername(e.target.value)}
                  />
              </div>
              <div className="form-group">
                  <label htmlFor="password">Password</label>
                  <input
                      type="password"
                      id="password"
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                  />
              </div>
              <button type="submit">Log in</button>
          </form>
      </div>
  );
})

export default LoginFormView;