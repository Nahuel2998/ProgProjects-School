vim.cmd [[packadd packer.nvim]]

return require('packer').startup(function()
  use 'wbthomason/packer.nvim'

  use 'tpope/vim-surround'
  use 'justinmk/vim-sneak'
end)
