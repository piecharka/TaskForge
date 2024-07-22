import { useState } from "react";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router-dom";
import "../style/LoginForm.css";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

const RegisterFormView = observer(function LoginFormView() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [birthday, setBirthday] = useState<Date | null>(null);
    const [error, setError] = useState<string | null>(null);
    const { userStore } = useStore();
    const navigate = useNavigate();

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        userStore.register({ username, password, email, birthday })
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
            <div className="first-part"></div>
            <div className="second-part">
                <h1>Register</h1>
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
                            type="email"
                            id="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            placeholder="email"
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
                    <div className="form-group">
                        <DatePicker
                            selected={birthday}
                            onChange={(date: Date | null) => setBirthday(date)}
                            placeholderText="Select a date"
                        />
                    </div>
                    <button type="submit">Submit</button>
                </form>
            </div>
        </div>
    );
})

export default RegisterFormView;

        //        public string Username { get; set; } = null!;

        //public string Email { get; set; } = null!;

        //public string PasswordHash { get; set; } = null!;

        //public DateTime Birthday { get; set; }