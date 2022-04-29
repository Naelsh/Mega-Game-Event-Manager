import { Navigate } from 'react-router-dom';

export default function Private (Component) {
    const auth = false; //your logic

    return auth ? <Component /> : <Navigate to="/login" />
}