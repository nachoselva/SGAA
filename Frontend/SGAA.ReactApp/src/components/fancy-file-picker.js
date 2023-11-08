import ArrowUpOnSquareIcon from '@heroicons/react/24/solid/ArrowUpOnSquareIcon';
import TrashIcon from '@heroicons/react/24/solid/TrashIcon';
import { Box, Button, Input, SvgIcon, TextField } from '@mui/material';
import React from 'react';
import { convertFileToBase64 } from '/src/utils/file-base64-converter';
import { FancyDownloadButton } from './fancy-download-button';

export const FancyFilePicker = (props) => {
  const { touched, error, name, label, onChange, onBlur, file, readOnly } = props;
  const currentFile = JSON.parse(file);

  const handleChange = async (file) => {
    if (file) {
      const result = await convertFileToBase64(file);
      if (result?.base64 !== currentFile?.base64 && result?.size < 200) {
        onChange(JSON.stringify(result));
      }
    }
    else {
      onChange(null);
    }
  }

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
          shrink: !!currentFile
        }}
      />
      {
        !readOnly &&
        !file &&
        <Button
          name={name}
          variant="contained"
          component="label"
          sx={{ 'width': '40px', 'minWidth': '40px', 'height': '40px', 'minHeight': '40px', 'marginTop': '7px' }}
          tabIndex={-1}
        >
          <Input type='file' onBlur={onBlur} onChange={async (event) => {
            const file = event.target.files[0];
            await handleChange(file);
          }}
          />
          <SvgIcon>
            <ArrowUpOnSquareIcon />
          </SvgIcon>
        </Button>
      }
      {
        file &&
        <FancyDownloadButton currentFile={currentFile} />
      }
      {
        !readOnly &&
        file &&
        <Button
          name={name}
          variant="contained"
          component="label"
          sx={{ 'width': '40px', 'minWidth': '40px', 'height': '40px', 'minHeight': '40px', 'marginTop': '7px' }}
          tabIndex={-1}
          onClick={async () => {
            await handleChange(null);
          }}
        >
          <SvgIcon>
            <TrashIcon />
          </SvgIcon>
        </Button>
      }
    </Box >
  );
}