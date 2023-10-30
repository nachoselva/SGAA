import { useSelector } from "react-redux";
import PropTypes from 'prop-types';

export const LoadingLayout = (props) => {
  const { children } = props;
  const loadingState = useSelector((state) => state.loading);

  const style = { position: "fixed", top: "50%", left: "50%", transform: "translate(-50%, -50%)" };

  return (
    <>
      {loadingState.loadingCount > 0 &&
        <div>
          Estoy cargando
        </div>
      }
      {children}
    </>
  );
};

LoadingLayout.prototypes = {
  children: PropTypes.node
};