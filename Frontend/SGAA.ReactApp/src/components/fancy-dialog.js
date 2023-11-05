import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Slide from '@mui/material/Slide';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="down" ref={ref} {...props} />;
});

export const FancyDialog = (props) => {
  const { title, content, options, opened, actionName, param } = props;

  const handleClose = (action) => {
    action(param);
  };

  return (
    <Dialog
      open={opened}
      TransitionComponent={Transition}
      aria-describedby="fancy-dialog"
    >
      <DialogTitle>{title}</DialogTitle>
      <DialogContent>
        <DialogContentText id={"fancy-dialog-" + actionName + "-" + param} >
          {content}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        {
          options.map((option, index) => <Button key={index} variant="contained" onClick={() => handleClose(option.action)}>{option.text}</Button>)
        }
      </DialogActions>
    </Dialog>
  );
}