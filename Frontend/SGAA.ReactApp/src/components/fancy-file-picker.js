import ArrowDownOnSquareIcon from '@heroicons/react/24/solid/ArrowDownOnSquareIcon';
import ArrowUpOnSquareIcon from '@heroicons/react/24/solid/ArrowUpOnSquareIcon';
import { Button, Input, Link, SvgIcon, TextField, Box } from '@mui/material';
import React from 'react';
import { convertFileToBase64 } from '/src/utils/file-base64-converter';

export const FancyFilePicker = (props) => {
  const { touched, error, name, label, onChange, onBlur, file } = props;
  const currentFile = JSON.parse(file);

  return (
    <Box sx={{ display: 'flex', alignItems: 'flex-start', justifyContent: 'space-between' }}>
      <TextField
        variant="filled"
        fullWidth
        error={!!(touched && error)}
        helperText={touched && error}
        label={label}
        name={name}
        value={currentFile?.name ?? ''}
        sx={{ width: '80%' }}
        InputProps={{
          readOnly: true
        }}
        InputLabelProps={{
          shrink: currentFile
        }}
      />
      <Button
        name={name}
        variant="contained"
        component="label"
        sx={{ 'width': '40px', 'min-width': '40px', 'height': '40px', 'min-height': '40px', 'margin-top': '7px' }}
        tabindex="-1"
      >
        <Input type='file' onBlur={onBlur} onChange={async (event) => {
          const file = event.target.files[0];
          const result = await convertFileToBase64(file);
          if (result?.base64 !== currentFile?.base64 && result?.size < 200) {
            onChange(JSON.stringify(result));
          }
        }}
        />
        <SvgIcon>
          <ArrowUpOnSquareIcon />
        </SvgIcon>
      </Button>
      <Link
        variant="contained"
        href={currentFile && currentFile.base64}
        download={currentFile && currentFile.name}
        sx={{ 'width': '40px', 'min-width': '40px', 'height': '40px', 'min-height': '40px', display: 'flex', alignItems: 'flex-start', justifyContent: 'space-between', 'margin-top': '7px' }}
      >
        <SvgIcon>
          <ArrowDownOnSquareIcon />
        </SvgIcon>
      </Link>
    </Box >
  );
}