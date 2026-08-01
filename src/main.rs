use std::env;
use filesize::PathExt;
use std::collections::HashMap;
use std::path::Path;

fn get_file_size(file: &Path) -> Result<u64, &str> {
    let size = file.size_on_disk();
}

fn get_dir_size(file: &Path) -> Result<u64, &str> {
    
}

fn main() {
    let mut file_compilations: HashMap<&str, &str> = HashMap::new();
    let dirs_and_files: Vec<String> = env::args().collect();

    for p in dirs_and_files {
        // Get the path
        let content = Path::new(&p);

        if !content.exists() {
            panic!("The path {} does not exist!", p);
        }

        let mut content_size: Result<u64, &str>;

        if content.is_file() {
            content_size = get_file_size(content);
        } else if content.is_dir() {
            content_size = get_dir_size(content);
        }


    }
}
