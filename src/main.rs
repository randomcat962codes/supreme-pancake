use std::env;
use std::fs;
use std::collections::HashMap;
use std::path::Path;

fn get_file_size(file: &Path) -> i64 {
    0 // Temprorary value until implementation
}

fn get_dir_size(file: &Path) -> i64 {
    0 // Temprorary value until implementation
}

fn main() {
    let mut file_compilations: HashMap<&str, i64> = HashMap::new();
    let dirs_and_files: Vec<String> = env::args().collect();

    for p in dirs_and_files {
        // Get the path
        let content = Path::new(&p);

        if !content.exists() {
            panic!("The path {} does not exist!", p);
        }

        let mut content_size: i64;

        if content.is_file() {
            content_size = get_file_size(content);
        } else if content.is_dir() {
            content_size = get_dir_size(content);
        }

        
    }
}
